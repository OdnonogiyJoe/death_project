using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Controls;
using WpfApp34.Pages;
using WpfApp34.ViewModels;

namespace WpfApp34.Tools
{
    static class Pager
    {
        static Dictionary<PageType, (Type, Type)> registerPages = new Dictionary<PageType, (Type, Type)>();
        static Dictionary<PageType, Page> pageInstances = new Dictionary<PageType, Page>();
        static Dictionary<(PageType, Type), Action<object>> messageTypes = new Dictionary<(PageType, Type), Action<object>>();
        public static void Register(PageType pageType, Type page, Type vm)
        {
            registerPages.Add(pageType, (page, vm));
        }

        public static Page GetPageByType(PageType pageType)
        {
            if (pageInstances.ContainsKey(pageType))
                return pageInstances[pageType];
            var registerPage = registerPages[pageType];
            IVM vm = (IVM)Activator.CreateInstance(registerPage.Item2);
            Page page = (Page)Activator.CreateInstance(registerPage.Item1, vm);
            pageInstances.Add(pageType, page);
            var action = vm.RegisterTypeMessage();
            messageTypes.Add((pageType, action.Item1), action.Item2);
            return page;
        }

        internal static void SendMessage(object message, PageType type)
        {
            var key = (type, message.GetType());
            if (messageTypes.ContainsKey(key))
                messageTypes[key](message);
        }

        internal static void GoTo(PageType pageType)
        {
            pageChange(pageType);
        }
        static Action<PageType> pageChange;
        internal static void RegisterPageChanger(Action<PageType> pageChange)
        {
            Pager.pageChange = pageChange;
        }

        internal static void RegisterPages()
        {
            registerPages.Add(PageType.PublisherEditor, (typeof(PublisherEditor), typeof(PublisherEditorVM)));
            registerPages.Add(PageType.GenreEditor, (typeof(GenreEditor), typeof(GenreEditorVM)));
            registerPages.Add(PageType.AuthorEditor, (typeof(AuthorEditor), typeof(AuthorEditorVM)));
            registerPages.Add(PageType.BookList, (typeof(BookList), typeof(BookListVM)));
            registerPages.Add(PageType.BookEdit, (typeof(BookEdit), typeof(BookEditVM)));
        }
    }
}
