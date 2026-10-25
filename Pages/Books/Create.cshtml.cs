using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Popescu_Dorian_Ioan_Lab2;
using Popescu_Dorian_Ioan_Lab2.Models;

namespace Popescu_Dorian_Ioan_Lab2.Pages.Books
{
    public class CreateModel : BookCategoriesPageModel
    {
        private readonly Popescu_Dorian_Ioan_Lab2Context _context;

        public CreateModel(Popescu_Dorian_Ioan_Lab2Context context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            // Am decomentat partea cu autorul pentru ca tu il ai din Laboratorul 2
            var authorList = _context.Author.Select(x => new
            {
                x.ID,
                FullName = x.LastName + " " + x.FirstName
            });
            ViewData["AuthorID"] = new SelectList(authorList, "ID", "FullName");

            ViewData["PublisherID"] = new SelectList(_context.Publisher, "ID", "PublisherName");

            var book = new Book();
            book.BookCategories = new List<BookCategory>();

            // Pregatim checkbox-urile pentru categorii
            PopulateAssignedCategoryData(_context, book);

            return Page();
        }

        [BindProperty]
        public Book Book { get; set; } = default!;

        public async Task<IActionResult> OnPostAsync(string[] selectedCategories)
        {
            var newBook = new Book();
            if (selectedCategories != null)
            {
                newBook.BookCategories = new List<BookCategory>();
                foreach (var cat in selectedCategories)
                {
                    var catToAdd = new BookCategory
                    {
                        CategoryID = int.Parse(cat)
                    };
                    newBook.BookCategories.Add(catToAdd);
                }
            }

            // Atasam categoriile selectate noii carti
            Book.BookCategories = newBook.BookCategories;

            _context.Book.Add(Book);
            await _context.SaveChangesAsync();

            // Am schimbat "../../Index" in "./Index" ca sa te duca la lista de carti
            return RedirectToPage("./Index");
        }
    }
}