﻿using Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Controller
{
    public class Race
    {
        public Track Track { get; set; }
        public List<IParticipant> Participants { get; set; }
        public DateTime StartTime { get; set; }

        private Random _random;

        private Dictionary<Section, SectionData> _positions;

        public SectionData GetSectionData(Section section)
        {
            // look for key section in dictionary, if exists, return sectiondata for key section.
            // otherwise, create SectionData for section and return that object.
            if (!_positions.ContainsKey(section)) _positions.Add(section, new SectionData());
            return _positions[section];
        }

        public Race(Track track, List<IParticipant> participants)
        {
            Track = track;
            Participants = participants;
            _random = new Random(DateTime.Now.Millisecond);
            _positions = new Dictionary<Section, SectionData>();

            PlaceParticipantsOnStartGrid();
        }

        public void RandomizeEquipment()
        {
            foreach (IParticipant participant in Participants)
            {
                participant.Equipment.Performance = _random.Next();
                participant.Equipment.Quality = _random.Next();
            }
        }

        public void PlaceParticipantsOnStartGrid()
        {
            // create List of startgrids, from front to back
            List<Section> startGrids = GetStartGrids();

            // look at amount of participants and amount of start places.
            int amountToPlace = 0;
            if (Participants.Count >= startGrids.Count * 2)
                amountToPlace = startGrids.Count * 2;
            else if (Participants.Count < startGrids.Count * 2)
                amountToPlace = Participants.Count;

            bool side = false; // false is left, true is right
            int currentStartGridIndex = 0;
            for (int i = 0; i < amountToPlace; i++)
            {
                // place
                PlaceParticipant(Participants[i], side, startGrids[currentStartGridIndex]);
                // flip side
                side = !side;
                // up section index on every uneven number for i
                if (i % 2 == 1)
                    currentStartGridIndex++;
            }

        }

        public List<Section> GetStartGrids()
        {
            List<Section> startGridSections = new List<Section>();

            // put all sections in list that have sectiontype StartGrid
            foreach (Section trackSection in Track.Sections)
            {
                if (trackSection.SectionType == SectionTypes.StartGrid)
                    startGridSections.Add(trackSection);
            }
            
            // reverse list
            startGridSections.Reverse();

            return startGridSections;
        }

        public void PlaceParticipant(IParticipant p, bool side, Section section)
        {
            if (side)
                GetSectionData(section).Right = p;
            else
                GetSectionData(section).Left = p;
        }
    }
}
