using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using System.Threading.Tasks;
using ApiClient.Model;
using System.Text.Json;
using System.Net.Http;
using Microsoft.UI.Xaml.Media.Imaging;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace ApiClient
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class PokemonDetails : Page
    {
        public PokemonDetails()
        {
            this.InitializeComponent();
        }

        protected override async void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            if (e.Parameter is string pokemonName)
            {
                // Fetch and display Pokémon details
                await FetchPokemonDetails(pokemonName);
            }
        }


        private async Task FetchPokemonDetails(string pokemonName)
        {
            using HttpClient client = new HttpClient();
            var response = await client.GetStringAsync($"https://pokeapi.co/api/v2/pokemon/{pokemonName.ToLower()}");
            System.Diagnostics.Debug.WriteLine(response);
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
            };

            var pokemonDetails = JsonSerializer.Deserialize<Pokemon>(response, options);

            // Update UI elements with fetched data
            PokemonNameTextBlock.Text = pokemonName;
            PokemonHeightTextBlock.Text = $"Height: {pokemonDetails.Height}";
            PokemonWeightTextBlock.Text = $"Weight: {pokemonDetails.Weight}";

            // Safely retrieve the artwork URL
            string artworkUrl = pokemonDetails?.Sprites?.Other?.OfficialArtwork?.Front_Default;
           
            if (!string.IsNullOrEmpty(artworkUrl))
            {
                PokemonArtworkImage.Source = new BitmapImage(new Uri(artworkUrl));
            }
            else
            { 
                PokemonArtworkImage.Source = null;
            }
        }


        private void Back_Click(object sender, RoutedEventArgs e)
        {
            Frame frame = new Frame();
            frame.Navigate(typeof(PokemonLists));
            this.Content = frame;
        }
    }
}
