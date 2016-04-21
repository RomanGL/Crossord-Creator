using CC.Core.Services.Interfaces;
using System.Collections.Generic;

namespace CCApp.Services
{
    public sealed class BlurService : IBlurService
    {
        public void Blur()
        {
            foreach (var b in _registrations)
            {
                b.Blur();
            }
        }

        public void UnBlur()
        {
            foreach (var b in _registrations)
            {
                b.UnBlur();
            }
        }

        public void Register(IBlurSupport blurSupport)
        {
            _registrations.Add(blurSupport);
        }

        public void UnRegister(IBlurSupport blurSupport)
        {
            _registrations.RemoveAll(b => b == blurSupport);
        }

        private readonly List<IBlurSupport> _registrations = new List<IBlurSupport>();
    }
}
