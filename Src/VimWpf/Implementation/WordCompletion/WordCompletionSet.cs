﻿using Microsoft.VisualStudio.Language.Intellisense;
using Microsoft.VisualStudio.Text;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Vim.UI.Wpf.Implementation.WordCompletion
{
    internal sealed class WordCompletionSet : CompletionSet
    {
        internal const string Name = "Words";

        internal WordCompletionSet()
        {

        }

        internal WordCompletionSet(ITrackingSpan wordTrackingSpan, IEnumerable<Completion> completions)
            : base(Name, Name, wordTrackingSpan, completions, null)
        {

        }

        public void AddExtra(IEnumerable<string> words)
        {
            var toAdd = words
                .Where(x => !ContainsCompletion(x))
                .Select(x => new Completion(x))
                .ToList();
            WritableCompletions.AddRange(toAdd);
        }

        private bool ContainsCompletion(string word)
        {
            var comparer = StringComparer.Ordinal;
            for (int i=  0; i < Completions.Count; i++)
            {
                if (comparer.Equals(Completions[i].DisplayText, word))
                {
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// For a word completion set there is no best match.  This is called very often by the the various
        /// pieces of the intellisense stack to select the best match based on the current data in the
        /// ITextBuffer.  It's meant to filter as the user types.  We don't want any of that behavior in 
        /// the word completion scenario
        /// </summary>
        public override void SelectBestMatch()
        {

        }
    }
}
