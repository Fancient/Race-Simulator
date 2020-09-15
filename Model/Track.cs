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

            // instantiate Sections LinkedList. Loop through SectionTypes[] parameter,
            // add new section of current section type to LinkedList.
            Sections = new LinkedList<Section>();
            foreach (SectionTypes sectionType in sections)
            {
                Sections.AddLast(new Section(sectionType));
            }
        }
    }
}
