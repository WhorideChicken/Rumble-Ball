using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Define;

public class PagingSystem : Singletone<PagingSystem>
{
    [SerializeField] private BasePage _mainMenu;
    [SerializeField] private BasePage _stagePage;
    [SerializeField] private BasePage _itemPage;
    [SerializeField] private BasePage _rankingPage;
    [SerializeField] private BasePage _creatorPage;

    private Dictionary<PageType, BasePage> _pageDictionary;
    private Stack<BasePage> _pageStack = new Stack<BasePage>();

    private void Start()
    {
        InitializePageDictionary();

        if (SceneLauncher.Instance.isFirst)
        {
            SceneLauncher.Instance.isFirst = false;
            InitializePages();
            ShowNextPage();
        }
        else
        {
            PushAndShowPage(PageType.Menu);
        }
    }

    private void InitializePageDictionary()
    {
        _pageDictionary = new Dictionary<PageType, BasePage>
        {
            { PageType.Menu, _mainMenu },
            { PageType.Stage, _stagePage },
            { PageType.Item, _itemPage },
            { PageType.Ranking, _rankingPage },
            { PageType.Creator, _creatorPage }
        };
    }

    private void InitializePages()
    {
        foreach (var page in _pageDictionary.Values)
        {
            _pageStack.Push(page);
        }
    }

    public void ShowNextPage()
    {
        if (_pageStack.Count > 1)
        {
            _pageStack.Pop().ShowPage(ShowNextPage);
        }
        else if (_pageStack.Count == 1)
        {
            _pageStack.Pop().ShowPage();
        }
    }

    public void ShowPreviousPage()
    {
        if (_pageStack.Count > 1)
        {
            _pageStack.Pop().HidePage();
            _pageStack.Peek().ShowPage();
        }
    }

    public void PushAndShowPage(PageType pageType)
    {
        if (_pageDictionary.TryGetValue(pageType, out BasePage page))
        {
            _pageStack.Push(page);
            page.ShowPage();
        }
        else
        {
            //Error
        }
    }
}
