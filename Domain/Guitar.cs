using System;

namespace Domain
{
    public class Guitar
    {
        public virtual Guid Id { get; set; }
        public virtual string Type { get; set; }
    }
}