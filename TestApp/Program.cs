using System;
using System.Collections.Generic;
using System.Linq;

namespace TestApp
{
    class Program
    {
        static void Main(string[] args)
        {
            List<StoryUnit> elements = new(new[] { new Scene() });
            List<Package<StoryUnit>> chilrens = new(new[] { new Package<StoryUnit>(null) });
            Package<StoryUnit> test = new Package<StoryUnit>(null,
                new[] { new Scene() }, new[] { new Package<StoryUnit>(null) });

        }

    static bool TryGetNextLink(StoryUnit element, out StoryUnit nextElement)
        {
            if (element.HasConditionalLinks) 
                foreach(ConditionalLink jc in element.ConditionalLinks)
                {
                    if (jc.Check(out StoryUnit link))
                    {
                        nextElement = link;
                        return true;
                    }
                }

            if (element.HasUnConditionalLink)
            {
                nextElement = element.UnConditionLink;
                return true;
            }

            nextElement = null;
            return false;
        }
    
    abstract class StoryUnit
    {
        public List<ConditionalLink> ConditionalLinks = new();
        public void AddJumpCondition(ConditionalLink jump) =>
            ConditionalLinks.Add(jump);
        public void AddConditionalLink(StoryUnit element, Func<bool> condition) =>
            ConditionalLinks.Add(new ConditionalLink(element, condition));

        public StoryUnit UnConditionLink;

        public bool HasUnConditionalLink
        {
            get => UnConditionLink != null;
        }
        public bool HasConditionalLinks
        {
            get => ConditionalLinks.Count > 0;
        }
    }

    class Scene : StoryUnit
    {
        public int id;
    }

    //class Fragment
    //{
    //    public string Name;
    //    public StoryUnit First
    //    {
    //        get => elements.FirstOrDefault();
    //    }
    //    public StoryUnit Last
    //    {
    //        get => elements.LastOrDefault();
    //    }
    //    public int Size
    //    {
    //        get => elements.Count;
    //    }

    //    public List<StoryUnit> Elements = new ();
    //    public List<Fragment> Childrens;
    //}

    class Package<ElementType>
    {
        List<ElementType> Elements;
        List<Package<ElementType>> Childrens;
        Package<ElementType> Parent;

        public Package(Package<ElementType> parent)
        {
            this.Parent = parent;
        }
        public Package(Package<ElementType> parent, IEnumerable<ElementType> elements) : this(parent)
        {
            this.Elements = new(elements);
        }
        public Package(Package<ElementType> parent, IEnumerable<ElementType> elements, IEnumerable<Package<ElementType>> childrens)
            : this(parent, elements)
        {
            this.Childrens = new(childrens);
        }
    }



    class ConditionalLink
    {
        StoryUnit link;
        Func<bool> Condition;
        public bool Check(out StoryUnit link)
        {
            if (Condition())
            {
                link = this.link;
                return true;
            }
            link = null;
            return false;
        }
        public ConditionalLink(StoryUnit link, Func<bool> condition)
        {
            this.link = link;
            this.Condition = condition;
        }

    }

    class Transition<LinkType> where LinkType : class
    {
        protected LinkType link;
        protected Func<bool> condition;
        public bool Check(out LinkType link)
        {
            if (condition is null)
            {
                link = this.link;
                return true;
            }
            else if (condition())
            {
                link = this.link;
                return true;
            }
            link = default;
            return false;
        }

        public Transition(LinkType link, Func<bool> condition = null)
        {
            this.link = link;
            this.condition = condition;
        }
    }

    //class MyCollection<ElementType>
    //{
    //    List<ElementType> elements;

    //    public void Add
    //}
    //class ConditionTransition<LinkType> : Transition<LinkType>
    //    where LinkType : class
    //{
    //    Func<bool> Condition;
    //    public override bool Check(out LinkType link)
    //    {
    //        if (Condition())
    //        {
    //            link = this.link;
    //            return true;
    //        }
    //        link = default;
    //        return false;
    //    }
    //    public ConditionTransition(LinkType link, Func<bool> condition) : base(link)
    //    {
    //        this.Condition = condition;
    //    }
    //}
    //class UnConditionTransition<LinkType> : Transition<LinkType>
    //    where LinkType : class
    //{
    //    public override bool Check(out LinkType link)
    //    {
    //        link = this.link;
    //        return true;
    //    }

    //    public UnConditionTransition(LinkType link) : base(link) { }
    }
}






