using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Timers;

[assembly: InternalsVisibleTo("Controller.Test")]

namespace Controller
{
    public class Race
    {
        public Track Track { get; set; }
        public List<IParticipant> Participants { get; set; }
        public DateTime StartTime { get; set; }
        
        private Random _random;
        private Dictionary<Section, SectionData> _positions;
        private Timer timer;
        private int timerInterval = 200;

        // keeping track of laps
        private const int Laps = 3;
        private Dictionary<IParticipant, int> _lapsDriven;
        private SectionData finishSectionData;
        private IParticipant finishPreviousLeft;
        private IParticipant finishPreviousRight;

        // event for drivers changed positions
        public event EventHandler<DriversChangedEventArgs> DriversChanged; 

        // event for race finish
        public event EventHandler RaceFinished;

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
            timer = new Timer(timerInterval);
            timer.Elapsed += OnTimedEvent;

            PlaceParticipantsOnStartGrid();
            InitializeParticipantLaps();
            InitializeFinishSectionData();
        }

        private void InitializeFinishSectionData()
        {
            // find finish section from track (there's only one finish)
            finishSectionData = GetSectionData(Track.Sections.First(a => a.SectionType == SectionTypes.Finish));
        }

        private void InitializeParticipantLaps()
        {
            _lapsDriven = new Dictionary<IParticipant, int>();
            // fill dictionary
            foreach (IParticipant participant in Participants)
            {
                _lapsDriven.Add(participant, 0);
            }
        }

        public void RandomizeEquipment()
        {
            foreach (IParticipant participant in Participants)
            {
                participant.Equipment.Performance = _random.Next(5, 16); //  5 <= performance <= 15
                participant.Equipment.Quality = _random.Next(1, 11); // 1 <= quality <= 10
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

        public void OnTimedEvent(object sender, EventArgs e)
        {
            // call method to change driverPositions.
            // Steps to take:
            // Traverse sections list from end to beginning. (this is so when a driver moves enough, a positions opens up for the driver behind
            LinkedListNode<Section> currentSectionNode = Track.Sections.Last;
            UpdateLaps();
            // check if any participants on track, if not, race is finished.
            if (CheckRaceFinished())
            {
                // TODO: Mess with ordering of events here to get it to display correctly
                RaceFinished?.Invoke(this, new EventArgs());
            }

            while (currentSectionNode != null)
            {
                // get sectiondata variables for current and next section
                SectionData currentSectionData = GetSectionData(currentSectionNode.Value);
                SectionData nextSectionData = GetSectionData(currentSectionNode.Next != null ? currentSectionNode.Next.Value : Track.Sections.First.Value);

                MoveParticipantsOnSectionData(currentSectionData, nextSectionData);
                

                // loop iterator
                currentSectionNode = currentSectionNode.Previous;
            }


            // raise driversChanged event.
            DriversChanged?.Invoke(this, new DriversChangedEventArgs(){Track = this.Track});
        }

        private void MoveParticipantsOnSectionData(SectionData currentSectionData, SectionData nextSectionData)
        {
            // first check participants on currentsectiondata and update distance.
            if (currentSectionData.Left != null)
            {
                currentSectionData.DistanceLeft += GetSpeedFromParticipant(currentSectionData.Left);
            }

            if (currentSectionData.Right != null)
            {
                currentSectionData.DistanceRight += GetSpeedFromParticipant(currentSectionData.Right);
            }


            // TODO: maak methodes voor verplaatsen: leftToLeft, leftToRight, rightToRight, rightToRight. parameters: 2 sectiondata, bool correctOtherDriver.
            if (currentSectionData.DistanceLeft >= 100 && currentSectionData.DistanceRight >= 100)
            {
                #region Both drivers ready to move

                int freePlaces = FreePlacesLeftOnSectionData(nextSectionData);
                if (freePlaces == 0)
                {
                    // no places available
                    currentSectionData.DistanceRight = 99;
                    currentSectionData.DistanceLeft = 99;
                }
                else if (freePlaces == 3)
                {
                    // move both
                    MoveParticipantTo(currentSectionData, nextSectionData, false, false, false);
                    MoveParticipantTo(currentSectionData, nextSectionData, true, true, false);
                }
                else
                {
                    // TODO: handle edge cases a little better when a choice must be made
                    if (currentSectionData.DistanceLeft >= currentSectionData.DistanceRight)
                    {
                        // prefer left
                        if (freePlaces == 1)
                            MoveParticipantTo(currentSectionData, nextSectionData, false, false, true); // left to left
                        else if (freePlaces == 2)
                            MoveParticipantTo(currentSectionData, nextSectionData, false, true, true);
                    }
                    else
                    {
                        // choose right
                        if (freePlaces == 1)
                            MoveParticipantTo(currentSectionData, nextSectionData, true, false, true); // left to left
                        else if (freePlaces == 2)
                            MoveParticipantTo(currentSectionData, nextSectionData, true, true, true);
                    }
                }

                #endregion
            }
            else if (currentSectionData.DistanceLeft >= 100)
            {
                #region Left driver ready to move
                // for freesections, prefer same spot, otherwise take other
                int freePlaces = FreePlacesLeftOnSectionData(nextSectionData);
                if (freePlaces == 0)
                    currentSectionData.DistanceLeft = 99;
                else if (freePlaces == 3 || freePlaces == 1)
                    // move from left to left
                    MoveParticipantTo(currentSectionData, nextSectionData, false, false, false);
                else if (freePlaces == 2)
                    // move from left to right
                    MoveParticipantTo(currentSectionData, nextSectionData, false, true, false);
                #endregion
            }
            else if (currentSectionData.DistanceRight >= 100)
            {
                #region Right driver ready to move
                // for freesections, prefer same spot, otherwise take other
                int freePlaces = FreePlacesLeftOnSectionData(nextSectionData);
                if (freePlaces == 0)
                    currentSectionData.DistanceRight = 99;
                else if (freePlaces == 3 || freePlaces == 2)
                    // move from right to right
                    MoveParticipantTo(currentSectionData, nextSectionData, true, true, false);
                else if (freePlaces == 1)
                    // move from right to left
                    MoveParticipantTo(currentSectionData, nextSectionData, true, false, false);
                #endregion
            }
        }

        public int GetSpeedFromParticipant(IParticipant iParticipant)
        {
            return Convert.ToInt32(Math.Ceiling(0.1 * iParticipant.Equipment.Speed * iParticipant.Equipment.Performance + 18));
        }

        private int FreePlacesLeftOnSectionData(SectionData sd)
        {
            // values possible:
            // 0: no places possible
            // 1: left free
            // 2: right free
            // 3: both free
            int returnval = 0;
            if (sd.Left == null)
                returnval += 1;
            if (sd.Right == null)
                returnval += 2;
            return returnval;
        }

        private void MoveParticipantTo(SectionData current, SectionData next, bool start, bool end, bool correctOtherSide)
        {
            // bools start and end represent left or right, left is false, right is true
            if (start)
            {
                // select right
                if (end)
                {
                    // go to right
                    next.Right = current.Right;
                    next.DistanceRight = current.DistanceRight - 100;
                }
                else
                {
                    // go to left
                    next.Left = current.Right;
                    next.DistanceLeft = current.DistanceRight - 100;
                }

                current.Right = null;
                current.DistanceRight = 0;


                if (correctOtherSide)
                {
                    // correct distance of left side
                    current.DistanceLeft = 99;
                }
            }
            else
            {
                // select left
                if (end)
                {
                    // go to right
                    next.Right = current.Left;
                    next.DistanceRight = current.DistanceLeft - 100;
                }
                else
                {
                    // go to left
                    next.Left = current.Left;
                    next.DistanceLeft = current.DistanceLeft - 100;
                }

                current.Left = null;
                current.DistanceLeft = 0;

                if (correctOtherSide)
                {
                    // correct distance of right side
                    current.DistanceRight = 99;
                }
            }
        }

        public int GetDistanceParticipant(IParticipant participant)
        {
            // TODO: Remove debugging method
            // debugging method
            SectionData partL = _positions.Values.FirstOrDefault(part => part.Left == participant);
            SectionData partR = _positions.Values.FirstOrDefault(part => part.Right == participant);

            if (partL != null)
                return partL.DistanceLeft;
            if (partR != null)
                return partR.DistanceRight;
            return 0;
        }

        public int GetLapsParticipant(IParticipant participant)
        {
            // TODO: Remove debugging method
            return _lapsDriven[participant];
        }

        private void UpdateLaps()
        {
            // check driver on finish section, if lap threshold is reached, pull em off section.
            // TODO: Make this own method, this is doing 2 seperate things (not SOLID)
            if (finishSectionData.Left != null && _lapsDriven[finishSectionData.Left] >= Laps)
                finishSectionData.Left = null;
            if (finishSectionData.Right != null && _lapsDriven[finishSectionData.Right] >= Laps)
                finishSectionData.Right = null;

            // look for a driver on finish section, if driver is on finish, up its lap
            // TODO: I'm noticing a DRY theme when I'm dealing with SectionData's Left and Right.
            if (finishSectionData.Left != null && finishSectionData.Left != finishPreviousLeft)
            {
                _lapsDriven[finishSectionData.Left]++;
                finishPreviousLeft = finishSectionData.Left;
            }
            if (finishSectionData.Right != null && finishSectionData.Right != finishPreviousRight)
            {
                _lapsDriven[finishSectionData.Right]++;
                finishPreviousRight = finishSectionData.Right;
            }
        }

        private bool CheckRaceFinished()
        {
            if (_positions.Values.FirstOrDefault(a => a.Left != null || a.Right != null) == null)
                return true;
            return false;
        }

        public void Start()
        {
            timer.Start();
        }

        public void CleanUp()
        {
            DriversChanged = null;
            timer.Stop();
            RaceFinished = null;
        }
    }
}
