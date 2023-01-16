﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.ExternalAPI
{
    public interface IMovieExternalAPI
    {
        Task<List<MovieExtDTO>> GetMovies(CancellationToken ct);
    }
}
