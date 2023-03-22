using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Models.Common;

public class PageRequest {
    public int Page { get; set; }
    public int PageSize { get; set; }

    public int Offset { get; set; }

    public PageRequest(int page, int pageSize = 10) {
        Page = page < 1 ? 1 : page;
        PageSize = pageSize < 1 ? 10 : pageSize;

        Offset = (Page - 1) * PageSize;
    }
}

