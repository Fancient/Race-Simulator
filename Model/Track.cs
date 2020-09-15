using System;
using System.Collections.Generic;
using System.Text;

namespace Model
{
    public class Track
    {
        public string Name { get; set; }
        public LinkedList<Section> Sections { get; set; }

        public Track(string name, SectionTypes[] sections)
        {
            Name = name;

            // instantiate Sections linkedlist. Then loop through array and fill linkedlist with Section objects.
            Sections = new LinkedList<Section>();
            foreach (SectionTypes sectionType in sections)
            {
                Sections.AddLast(new Section(sectionType));
            }
        }
    }
}
