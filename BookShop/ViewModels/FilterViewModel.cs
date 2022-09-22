using Microsoft.AspNetCore.Mvc.Rendering;

namespace WebUI.ViewModels
{
    public class FilterViewModel
    {
        public FilterViewModel(List<string> genres, string genre, string name)
        {
            genres.Insert(0, "Все");
            Genres = new SelectList(genres, genre);
            SelectedGenre = genre;
            SelectedName = name;    
        }

        public SelectList Genres { get; set; }
        public string SelectedGenre { get; set; }
        public string SelectedName { get; set; }
    }
}
