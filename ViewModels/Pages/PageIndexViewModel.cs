using WebAppBackend.ViewModels.Pages;

public class PageIndexViewModel
{
    public List<PageViewModel> Pages { get; set; } = new();

    public List<PageViewModel> OrphanedPages { get; set; } = new();

    public int InvisiblePageCount { get; set; }

    public int OrphanPageCount { get; set; }
}