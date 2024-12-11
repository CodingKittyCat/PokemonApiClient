using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using System.Text.Json;
using ApiClient.Model;
using System.Threading.Tasks;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace ApiClient
{
    /// <summary>
    /// An empty window that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainWindow : Window
    {
        private List<PokemonResult> allPokemon;
        private List<PokemonResult> filteredPokemon;
        public MainWindow()
        {
            this.InitializeComponent();
            LoadPokemon();
        }

        private async void LoadPokemon()
        {
            allPokemon = await FetchPokemonAsync(); // Fetch all Pokémon
            filteredPokemon = new List<PokemonResult>(allPokemon); // Initialize filtered list
            PokemonList.ItemsSource = filteredPokemon; // Bind the filtered list to ListView

        }

        private void SearchBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            var query = SearchBox.Text.ToLower(); // Get search query

            // Filter the Pokémon list based on the search query
            filteredPokemon = allPokemon
                .Where(p => p.Name.ToLower().Contains(query)) // Case-insensitive search
                .ToList();

            // Update the ListView's ItemsSource with the filtered list
            PokemonList.ItemsSource = filteredPokemon;
        }


        public static async Task<List<PokemonResult>> FetchPokemonAsync()
        {
            using HttpClient client = new HttpClient();
            var response = await client.GetStringAsync("https://pokeapi.co/api/v2/pokemon?limit=2000");

            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
            };

            var pokemonData = JsonSerializer.Deserialize<Pokemon>(response, options);

            return pokemonData?.Results ?? new List<PokemonResult>();
        }

    }
}
