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
            //bool LikeJump() => vars["Test"] == 1;
            //bool DontLikeJump() => vars["Test"] == 0;
            //bool UnCondition() => true;

            //Scene s1 = new Scene() { id = 1 };
            //Scene s2 = new Scene() { id = 2 };
            //Scene s3 = new Scene() { id = 3 };
            //Scene s4 = new Scene() { id = 4 };

            //s1.JumpConditions.Add(new JumpCondition(s2, LikeJump));
            //s1.JumpConditions.Add(new JumpCondition(s3, DontLikeJump));
            //s1.JumpConditions.Add(new JumpCondition(s4, UnCondition));

            vars["Test"] = 1;
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
            Hello[2].AddJumpCondition(Hello[5], () => { return vars["Test"] == 1; });

            StoryUnit currentUnit = Hello.First;

            while(!currentUnit.IsLast)
            {
                Console.WriteLine((currentUnit as Scene).id);
                if (TryGetNextLink(currentUnit, out StoryUnit nextElement))
                    currentUnit = nextElement;
            }
        }

        static bool TryGetNextLink(StoryUnit element, out StoryUnit nextElement)
        {
            if (element.HasJumpConditions) 
                foreach(JumpCondition jc in element.JumpConditions)
                {
                    if (jc.Check(out StoryUnit link))
                    {
                        nextElement = link;
                        return true;
                    }
                }
            if (!element.IsLast)
            {
                nextElement = element.NextElement;
                return true;
            }

            nextElement = null;
            return false;
        }
    }
    abstract class StoryUnit
    {
        public List<JumpCondition> JumpConditions = new();
        public void AddJumpCondition(JumpCondition jump) =>
            JumpConditions.Add(jump);

        public void AddJumpCondition(StoryUnit element, Func<bool> condition) =>
            JumpConditions.Add(new JumpCondition(element, condition));

        public StoryUnit NextElement;

        public bool IsLast
        {
            get => NextElement == null;
        }
        public bool HasJumpConditions
        {
            get => JumpConditions.Count > 0;
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
            //Last?.JumpConditions.Add(new JumpCondition(element, new Func<bool>(() => { return true; })));
            if(Last != null) Last.NextElement = element;
            elements.Add(element);
        }
        public StoryUnit this[int number]
        {
            get => elements[number];
        }


    }


    class JumpCondition
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
        public JumpCondition(StoryUnit link, Func<bool> condition)
        {
            this.link = link;
            this.Condition = condition;
        }

    }


}
