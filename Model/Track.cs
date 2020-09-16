﻿using System;
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
            Sections = GenerateSections(sections);

        }

        public LinkedList<Section> GenerateSections(SectionTypes[] sectionTypes)
        {
            LinkedList<Section> sections = new LinkedList<Section>();
            foreach (SectionTypes sectionType in sectionTypes)
            {
                sections.AddLast(new Section(sectionType));
            }

            return sections;
        }
    }
}
