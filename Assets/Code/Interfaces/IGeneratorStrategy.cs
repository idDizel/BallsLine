using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BallsLine.Interfaces
{
    public interface IGeneratorStrategy
    {
        IEnumerable<IElement> Generate(int count);
        IEnumerable<IElementNotifier> Instantiate(IEnumerable<IElement> elements, Func<IElement, IElementNotifier> instantiator);
        void Save(IEnumerable<IElementNotifier> instances);
    }
}
