using System;
using System.Collections.Generic;
using System.Linq;

namespace TestApp
{
    class Program
    {
        static void Main(string[] args)
        {
            Dictionary<string, int> vars = new Dictionary<string, int>()
            {
                ["Test"] = 0
            };

            vars["Test"] = 2;
            Fragment Hello = new Fragment();
            Hello.Add(new Scene()
            {
                id = 1
            });
            Hello.Add(new Scene()
            {
                id = 2
            });
            Hello.Add(new Scene()
            {
                id = 3
            });
            Hello.Add(new Scene()
            {
                id = 4
            });
            Hello.Add(new Scene()
            {
                id = 5
            });
            Hello.Add(new Scene()
            {
                id = 6
            });
            Hello.Add(new Scene()
            {
                id = 7
            });
            Hello[2].AddConditionalLink(Hello[5], () => { return vars["Test"] == 1; });

            StoryUnit currentUnit = Hello.First;

            while(!currentUnit.HasUnConditionalLink)
            {
                Console.WriteLine((currentUnit as Scene).id);
                if (TryGetNextLink(currentUnit, out StoryUnit nextElement))
                    currentUnit = nextElement;
            }
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

            if (!element.HasUnConditionalLink)
            {
                nextElement = element.UnConditionLink;
                return true;
            }

            nextElement = null;
            return false;
        }
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
            get => UnConditionLink == null;
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

    class Fragment
    {
        public string Name;
        public StoryUnit First
        {
            get => elements.FirstOrDefault();
        }
        public StoryUnit Last
        {
            get => elements.LastOrDefault();
        }
        public int Size
        {
            get => elements.Count;
        }

        List<StoryUnit> elements = new ();

        public void Add(StoryUnit element)
        {
            if(Last != null) Last.UnConditionLink = element;
            elements.Add(element);
        }
        public StoryUnit this[int number]
        {
            get => elements[number];
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


}
