namespace hopital.Models;
using System;
using System.Collections.Generic;

public class PaginatedListViewModel<T>
{
    public List<T> Items { get; set; }
    public int PageIndex { get; set; }
    public int TotalPages { get; set; }

    public PaginatedListViewModel(List<T> items, int count, int pageIndex, int pageSize)
    {
        PageIndex = pageIndex;
        TotalPages = (int)Math.Ceiling(count / (double)pageSize);
        Items = items;
    }

    public bool HasPreviousPage
    {
        get { return (PageIndex > 1); }
    }

    public bool HasNextPage
    {
        get { return (PageIndex < TotalPages); }
    }
}

