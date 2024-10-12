using CM.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace CM.SaveFile
{
    public class SaveFileParser
    {
        public DateTime? GameDate;
        public List<CMNation> Nations;
        public List<CMDivision> Divisions;
        public List<CMClub> Clubs;
        public List<CMStaff> Staffs;
        public IDictionary<CMStaff, long> StaffOffsets = new Dictionary<CMStaff, long>();
        public IDictionary<CMPlayer, long> PlayerOffsets = new Dictionary<CMPlayer, long>();

        public void Save(string filename, IList<CMStaff> staffs = null)
        {
            if (staffs == null) staffs = Staffs;
            using (FileStream fs = new FileStream(filename, FileMode.Open, FileAccess.Write))
            using (BinaryWriter writer =  new BinaryWriter(fs))
            {
                foreach (CMStaff staff in staffs)
                {
                    // Staff fields.
                    writer.Seek((int)(StaffOffsets[staff] + 86), SeekOrigin.Begin);
                    writer.Write(staff.Adaptability);
                    writer.Write(staff.Ambition);
                    writer.Write(staff.Determination);
                    writer.Write(staff.Loyality);
                    writer.Write(staff.Pressure);
                    writer.Write(staff.Professionalism);
                    writer.Write(staff.Sportsmanship);
                    writer.Write(staff.Temperament);

                    // Player fields.
                    CMPlayer player = staff.Player;
                    if (player != null)
                    {
                        writer.Seek((int)(PlayerOffsets[player] + 5), SeekOrigin.Begin);
                        writer.Write(player.CurrentAbility);
                        writer.Write(player.PotentialAbility);
                        writer.Write(player.HomeReputation);
                        writer.Write(player.CurrentReputation);
                        writer.Write(player.WorldReputation);
                        writer.Write(player.Goalkeeper);
                        writer.Write(player.Sweeper);
                        writer.Write(player.Defender);
                        writer.Write(player.DefensiveMidfielder);
                        writer.Write(player.Midfielder);
                        writer.Write(player.AttackingMidfielder);
                        writer.Write(player.Attacker);
                        writer.Write(player.WingBack);
                        writer.Write(player.RightSide);
                        writer.Write(player.LeftSide);
                        writer.Write(player.CentreSide);
                        writer.Write(player.FreeRole);
                        writer.Write(player.Acceleration);
                        writer.Write(player.Aggression);
                        writer.Write(player.Agility);
                        writer.Write(player.Anticipation);
                        writer.Write(player.Balance);
                        writer.Write(player.Bravery);
                        writer.Write(player.Consistency);
                        writer.Write(player.Corners);
                        writer.Write(player.Crossing);
                        writer.Write(player.Decisions);
                        writer.Write(player.Dirtiness);
                        writer.Write(player.Dribbling);
                        writer.Write(player.Finishing);
                        writer.Write(player.Flair);
                        writer.Write(player.SetPieces);
                        writer.Write(player.Handling);
                        writer.Write(player.Heading);
                        writer.Write(player.ImportantMatches);
                        writer.Write(player.InjuryProneness);
                        writer.Write(player.Jumping);
                        writer.Write(player.Influence);
                        writer.Write(player.LeftFoot);
                        writer.Write(player.LongShots);
                        writer.Write(player.Marking);
                        writer.Write(player.OffTheBall);
                        writer.Write(player.NaturalFitness);
                        writer.Write(player.OneOnOnes);
                        writer.Write(player.Pace);
                        writer.Write(player.Passing);
                        writer.Write(player.Penalties);
                        writer.Write(player.Positioning);
                        writer.Write(player.Reflexes);
                        writer.Write(player.RightFoot);
                        writer.Write(player.Stamina);
                        writer.Write(player.Strength);
                        writer.Write(player.Tackling);
                        writer.Write(player.Teamwork);
                        writer.Write(player.Technique);
                        writer.Write(player.ThrowIns);
                        writer.Write(player.Versatility);
                        writer.Write(player.Creativity);
                        writer.Write(player.WorkRate);
                    }
                }
            }
        }

        public void Load(string filename)
        {
            Dictionary<String, Int32> clubNameToClubIdMap = null;
            IDictionary<String, Int32> divisionNameToDivisionIdMap = null;
            Dictionary<Int32, List<Int32>> squadIdToClubIdsMap = null;

            using (Stream stream = new FileStream(filename, FileMode.Open, FileAccess.Read))
            {
                using (BinaryReader binaryReader = new BinaryReader(stream))
                {
                    bool isCompressed = binaryReader.ReadInt32() == 4;
                    if (isCompressed) return;

                    // Skip 4 bytes
                    binaryReader.ReadBytes(4);

                    int blocksCount = binaryReader.ReadInt32();
                    List<CMBlock> blocks = new List<CMBlock>();

                    for (int i = 0; i < blocksCount; ++i)
                    {
                        CMBlock block = new CMBlock
                        {
                            Position = binaryReader.ReadInt32(),
                            Size = binaryReader.ReadInt32(),
                            Name = ReadCMString(binaryReader, 260),
                        };

                        blocks.Add(block);
                    }

                    CMBinaryReader br = new CMBinaryReader(binaryReader, isCompressed);

                    // general.dat
                    {
                        CMBlock block = blocks.Find(item => item.Name == "general.dat");

                        br.Seek(block.Position, SeekOrigin.Begin);

                        br.ReadBytes(3944);

                        GameDate = ReadDateTime(br);
                    }

                    // nation.dat
                    Nations = new List<CMNation>();

                    {
                        CMBlock block = blocks.Find(item => item.Name == "nation.dat");

                        br.Seek(block.Position, SeekOrigin.Begin);

                        int nationsCount = block.Size / 290;

                        for (int i = 0; i < nationsCount; ++i)
                        {
                            CMNation nation = new CMNation
                            {
                                Id = br.ReadInt32(),
                                Name = ReadCMString(br, 51),
                                GenderName = br.ReadSByte(),
                                ShortName = ReadCMString(br, 26),
                                GenderShortName = br.ReadSByte(),
                                Code = ReadCMString(br, 4),
                                Nationality = ReadCMString(br, 26),
                                ContinentId = br.ReadInt32(),
                                Region = br.ReadSByte(),
                                ActualRegion = br.ReadSByte(),
                                FirstLanguage = br.ReadSByte(),
                                SecondLanguage = br.ReadSByte(),
                                ThirdLanguage = br.ReadSByte(),
                                CapitalId = br.ReadInt32(),
                                StateOfDevelopment = br.ReadSByte(),
                                GroupMembership = br.ReadSByte(),
                                NationalStadiumId = br.ReadInt32(),
                                GameImportance = br.ReadSByte(),
                                LeagueStandard = br.ReadSByte(),
                                NumberClubs = br.ReadInt16(),
                                NumberStaff = br.ReadInt32(),
                                UpdateDay = br.ReadInt16(),
                                Reputation = br.ReadInt16(),
                                ForeColour1Id = br.ReadInt32(),
                                BackColour1Id = br.ReadInt32(),
                                ForeColour2Id = br.ReadInt32(),
                                BackColour2Id = br.ReadInt32(),
                                ForeColour3Id = br.ReadInt32(),
                                BackColour3Id = br.ReadInt32(),
                                FIFACoefficient = br.ReadDouble(),
                                FIFACoefficient91 = br.ReadDouble(),
                                FIFACoefficient92 = br.ReadDouble(),
                                FIFACoefficient93 = br.ReadDouble(),
                                FIFACoefficient94 = br.ReadDouble(),
                                FIFACoefficient95 = br.ReadDouble(),
                                FIFACoefficient96 = br.ReadDouble(),
                                UEFACoefficient91 = br.ReadDouble(),
                                UEFACoefficient92 = br.ReadDouble(),
                                UEFACoefficient93 = br.ReadDouble(),
                                UEFACoefficient94 = br.ReadDouble(),
                                UEFACoefficient95 = br.ReadDouble(),
                                UEFACoefficient96 = br.ReadDouble(),
                                RivalNation1Id = br.ReadInt32(),
                                RivalNation2Id = br.ReadInt32(),
                                RivalNation3Id = br.ReadInt32(),
                                LeagueSelected = br.ReadSByte(),
                                ShortlistOffset = br.ReadInt32(),
                                GamesPlayed = br.ReadSByte(),
                            };

                            Nations.Add(nation);

                            //Debug.WriteLine("Nation.  {0,3}  {1}  {2,-20}  {3}", nation.Id, nation.Code, nation.ShortName, nation.Name);
                        }
                    }

                    // club_comp.dat
                    Divisions = new List<CMDivision>();
                    divisionNameToDivisionIdMap = new Dictionary<string, int>();

                    {
                        CMBlock block = blocks.Find(item => item.Name == "club_comp.dat");

                        br.Seek(block.Position, SeekOrigin.Begin);

                        int divisionsCount = block.Size / 107;

                        for (int i = 0; i < divisionsCount; ++i)
                        {
                            CMDivision division = new CMDivision
                            {
                                Id = br.ReadInt32(),
                                Name = ReadCMString(br, 51),
                                GenderName = br.ReadSByte(),
                                ShortName = ReadCMString(br, 26),
                                GenderShortName = br.ReadSByte(),
                                Code = ReadCMString(br, 4),
                                Scope = br.ReadSByte(),
                                Selected = br.ReadSByte(),
                                ContinentId = br.ReadInt32(),
                                NationId = br.ReadInt32(),
                                ForeColourId = br.ReadInt32(),
                                BackColourId = br.ReadInt32(),
                                Reputation = br.ReadInt16(),
                            };

                            division.Nation = GetNationFromId(division.NationId, Nations);

                            Divisions.Add(division);

                            if (divisionNameToDivisionIdMap.ContainsKey(division.Name))
                            {
                            }
                            else
                            {
                                divisionNameToDivisionIdMap.Add(division.Name, division.Id);
                            }
                        }
                    }

                    // club.dat
                    Clubs = new List<CMClub>();
                    clubNameToClubIdMap = new Dictionary<string, int>();
                    squadIdToClubIdsMap = new Dictionary<int, List<int>>();

                    {
                        CMBlock block = blocks.Find(item => item.Name == "club.dat");

                        br.Seek(block.Position, SeekOrigin.Begin);

                        int clubsCount = block.Size / 581;

                        for (int i = 0; i < clubsCount; ++i)
                        {
                            CMClub club = new CMClub
                            {
                                Id = br.ReadInt32(),
                                Name = ReadCMString(br, 51),
                                GenderName = br.ReadSByte(),
                                ShortName = ReadCMString(br, 26),
                                GenderShortName = br.ReadSByte(),
                                NationId = br.ReadInt32(),
                                DivisionId = br.ReadInt32(),
                                LastDivisionId = br.ReadInt32(),
                                LastPosition = br.ReadSByte(),
                                ReserveDivisionId = br.ReadInt32(),
                                ProfessionalStatus = br.ReadSByte(),
                                Cash = br.ReadInt32(),
                                StadiumId = br.ReadInt32(),
                                OwnStadium = br.ReadSByte(),
                                ReserveStadiumId = br.ReadInt32(),
                                MatchDay = br.ReadSByte(),
                                Attendance = br.ReadInt32(),
                                MinAttendance = br.ReadInt32(),
                                MaxAttendance = br.ReadInt32(),
                                Training = br.ReadSByte(),
                                Reputation = br.ReadInt16(),
                                PLC = br.ReadSByte(),
                                ForeColour1Id = br.ReadInt32(),
                                BackColour1Id = br.ReadInt32(),
                                ForeColour2Id = br.ReadInt32(),
                                BackColour2Id = br.ReadInt32(),
                                ForeColour3Id = br.ReadInt32(),
                                BackColour3Id = br.ReadInt32(),
                                FavStaff1Id = br.ReadInt32(),
                                FavStaff2Id = br.ReadInt32(),
                                FavStaff3Id = br.ReadInt32(),
                                DisStaff1Id = br.ReadInt32(),
                                DisStaff2Id = br.ReadInt32(),
                                DisStaff3Id = br.ReadInt32(),
                                Rival1Id = br.ReadInt32(),
                                Rival2Id = br.ReadInt32(),
                                Rival3Id = br.ReadInt32(),
                                ChairmanId = br.ReadInt32(),
                                DirectorsIds = ReadIds(br, 3),
                                ManagerId = br.ReadInt32(),
                                AssistantManagerId = br.ReadInt32(),
                                SquadIds = ReadIds(br, 50),
                                CoachIds = ReadIds(br, 5),
                                ScoutIds = ReadIds(br, 7),
                                PhysioIds = ReadIds(br, 3),
                                EuroFlag = br.ReadInt32(),
                                EuroSeeding = br.ReadSByte(),
                                TeamSelectedIds = ReadIds(br, 20),
                                TacticTrainingIds = ReadIds(br, 4),
                                TacticSelected = br.ReadInt32(),
                                HasLinkedClub = br.ReadSByte(),
                            };

                            club.Nation = GetNationFromId(club.NationId, Nations);

                            Clubs.Add(club);

                            //Debug.WriteLine("Club. {0,6} {1}", club.Id, club.Name);

                            if (clubNameToClubIdMap.ContainsKey(club.Name))
                            {
                            }
                            else
                            {
                                clubNameToClubIdMap.Add(club.Name, club.Id);
                            }

                            foreach (int squadId in club.SquadIds)
                            {
                                if (squadId >= 0)
                                {
                                    if (squadIdToClubIdsMap.TryGetValue(squadId, out List<int> clubIds))
                                    {
                                        clubIds.Add(club.Id);
                                    }
                                    else
                                    {
                                        squadIdToClubIdsMap.Add(squadId, new List<int> { club.Id, });
                                    }
                                }
                            }
                        }
                    }

                    // names
                    Func<CMBinaryReader, string, List<CMName>> loadNames =
                        (br_, blockName) =>
                        {
                            List<CMName> names = new List<CMName>();

                            CMBlock block = blocks.Find(item => item.Name == blockName);

                            br_.Seek(block.Position, SeekOrigin.Begin);

                            int namesCount = block.Size / 60;

                            for (int i = 0; i < namesCount; ++i)
                            {
                                CMName name = new CMName
                                {
                                    Name = ReadCMString(br_, 51),
                                    Id = br_.ReadInt32(),
                                    NationId = br_.ReadInt32(),
                                    Count = br_.ReadSByte(),
                                };

                                names.Add(name);
                            }

                            return names;
                        };

                    List<CMName> firstNames = loadNames(br, "first_names.dat");
                    List<CMName> secondNames = loadNames(br, "second_names.dat");
                    List<CMName> commonNames = loadNames(br, "common_names.dat");

                    // player.dat
                    List<CMPlayer> players = new List<CMPlayer>();

                    {
                        CMBlock block = blocks.Find(item => item.Name == "player.dat");

                        br.Seek(block.Position, SeekOrigin.Begin);

                        int playersCount = block.Size / 70;

                        for (int i = 0; i < playersCount; ++i)
                        {
                            long offset = br.Position;
                            CMPlayer player = new CMPlayer
                            {
                                Id = br.ReadInt32(),
                                SquardNumber = br.ReadByte(),
                                CurrentAbility = br.ReadInt16(),
                                PotentialAbility = br.ReadInt16(),
                                HomeReputation = br.ReadInt16(),
                                CurrentReputation = br.ReadInt16(),
                                WorldReputation = br.ReadInt16(),
                                Goalkeeper = br.ReadSByte(),
                                Sweeper = br.ReadSByte(),
                                Defender = br.ReadSByte(),
                                DefensiveMidfielder = br.ReadSByte(),
                                Midfielder = br.ReadSByte(),
                                AttackingMidfielder = br.ReadSByte(),
                                Attacker = br.ReadSByte(),
                                WingBack = br.ReadSByte(),
                                RightSide = br.ReadSByte(),
                                LeftSide = br.ReadSByte(),
                                CentreSide = br.ReadSByte(),
                                FreeRole = br.ReadSByte(),
                                Acceleration = br.ReadSByte(),
                                Aggression = br.ReadSByte(),
                                Agility = br.ReadSByte(),
                                Anticipation = br.ReadSByte(),
                                Balance = br.ReadSByte(),
                                Bravery = br.ReadSByte(),
                                Consistency = br.ReadSByte(),
                                Corners = br.ReadSByte(),
                                Crossing = br.ReadSByte(),
                                Decisions = br.ReadSByte(),
                                Dirtiness = br.ReadSByte(),
                                Dribbling = br.ReadSByte(),
                                Finishing = br.ReadSByte(),
                                Flair = br.ReadSByte(),
                                SetPieces = br.ReadSByte(),
                                Handling = br.ReadSByte(),
                                Heading = br.ReadSByte(),
                                ImportantMatches = br.ReadSByte(),
                                InjuryProneness = br.ReadSByte(),
                                Jumping = br.ReadSByte(),
                                Influence = br.ReadSByte(),
                                LeftFoot = br.ReadSByte(),
                                LongShots = br.ReadSByte(),
                                Marking = br.ReadSByte(),
                                OffTheBall = br.ReadSByte(),
                                NaturalFitness = br.ReadSByte(),
                                OneOnOnes = br.ReadSByte(),
                                Pace = br.ReadSByte(),
                                Passing = br.ReadSByte(),
                                Penalties = br.ReadSByte(),
                                Positioning = br.ReadSByte(),
                                Reflexes = br.ReadSByte(),
                                RightFoot = br.ReadSByte(),
                                Stamina = br.ReadSByte(),
                                Strength = br.ReadSByte(),
                                Tackling = br.ReadSByte(),
                                Teamwork = br.ReadSByte(),
                                Technique = br.ReadSByte(),
                                ThrowIns = br.ReadSByte(),
                                Versatility = br.ReadSByte(),
                                Creativity = br.ReadSByte(),
                                WorkRate = br.ReadSByte(),
                                Morale = br.ReadSByte(),
                            };

                            players.Add(player);
                            PlayerOffsets[player] = offset;
                        }
                    }

                    // staff.dat
                    Staffs = new List<CMStaff>();

                    {
                        CMBlock block = blocks.Find(item => item.Name == "staff.dat");

                        br.Seek(block.Position, SeekOrigin.Begin);

                        int staffsCount = block.Size / 110;

                        for (int i = 0; i < staffsCount; ++i)
                        {
                            long offset = br.Position;
                            CMStaff staff = new CMStaff
                            {
                                Id = br.ReadInt32(),
                                FirstNameId = br.ReadInt32(),
                                SecondNameId = br.ReadInt32(),
                                CommonNameId = br.ReadInt32(),
                                DateOfBirth = ReadDateTime(br),
                                YearOfBirth = br.ReadInt16(),
                                FirstNationId = br.ReadInt32(),
                                SecondNationId = br.ReadInt32(),
                                IntApps = br.ReadByte(),
                                IntGoals = br.ReadByte(),
                                NationalJobId = br.ReadInt32(),
                                JobForNation = br.ReadSByte(),
                                DateJoinedNation = ReadDateTime(br),
                                DateExpiresNation = ReadDateTime(br),
                                ClubJobId = br.ReadInt32(),
                                JobForClub = br.ReadSByte(),
                                DateJoinedClub = ReadDateTime(br),
                                DateExpiresClub = ReadDateTime(br),
                                Wage = br.ReadInt32(),
                                Value = br.ReadInt32(),
                                Adaptability = br.ReadSByte(),
                                Ambition = br.ReadSByte(),
                                Determination = br.ReadSByte(),
                                Loyality = br.ReadSByte(),
                                Pressure = br.ReadSByte(),
                                Professionalism = br.ReadSByte(),
                                Sportsmanship = br.ReadSByte(),
                                Temperament = br.ReadSByte(),
                                PlayingSquad = br.ReadSByte(),
                                Classification = br.ReadSByte(),
                                ClubValuation = br.ReadSByte(),
                                PlayerId = br.ReadInt32(),
                                StaffPreferencesId = br.ReadInt32(),
                                NonPlayerId = br.ReadInt32(),
                                SquadSelectedFor = br.ReadSByte(),
                            };

                            staff.FirstName = GetNameFromId(staff.FirstNameId, firstNames);
                            staff.SecondName = GetNameFromId(staff.SecondNameId, secondNames);
                            staff.CommonName = GetNameFromId(staff.CommonNameId, commonNames);
                            staff.FirstNation = GetNationFromId(staff.FirstNationId, Nations);
                            staff.SecondNation = staff.SecondNationId != staff.FirstNationId ? GetNationFromId(staff.SecondNationId, Nations) : null;
                            staff.ClubJob = GetClubFromId(staff.ClubJobId, Clubs);
                            staff.Player = GetPlayerFromId(staff.PlayerId, players);
                            staff.Age = staff.DateOfBirth != null ? DiffInYears(staff.DateOfBirth.Value, GameDate.Value) : (int?)null;

                            Staffs.Add(staff);
                            StaffOffsets[staff] = offset;
                        }
                    }
                }
            }
        }

        public static Boolean IsValidPlayer(CMStaff staff)
        {
            return ((staff.Player != null) &&
                    (!String.IsNullOrEmpty(staff.FirstName?.Name) ||
                     !String.IsNullOrEmpty(staff.SecondName?.Name) ||
                     !String.IsNullOrEmpty(staff.CommonName?.Name)));
        }

        public static int DiffInYears(DateTime dateFrom, DateTime dateTo)
        {
            int m = dateFrom <= dateTo ? 1 : -1;

            DateTime dateFrom_ = m > 0 ? dateFrom : dateTo;
            DateTime dateTo_ = m > 0 ? dateTo : dateFrom;

            return ((new DateTime(1, 1, 1) + (dateTo_ - dateFrom_)).Year - 1) * m;
        }

        private int[] ReadIds(CMBinaryReader br, int size)
        {
            int[] ids = new int[size];

            for (int i = 0; i < size; ++i)
            {
                ids[i] = br.ReadInt32();
            }

            return ids;
        }

        private string ReadCMString(BinaryReader br, int size)
        {
            byte[] bb = br.ReadBytes(size);

            int c = 0;

            foreach (byte b in bb)
            {
                if (b == 0)
                {
                    break;
                }

                ++c;
            }

            return Encoding.GetEncoding("iso-8859-1").GetString(bb, 0, c);
        }

        private string ReadCMString(CMBinaryReader br, int size)
        {
            byte[] bb = br.ReadBytes(size);

            int c = 0;

            foreach (byte b in bb)
            {
                if (b == 0)
                {
                    break;
                }

                ++c;
            }

            return Encoding.GetEncoding("iso-8859-1").GetString(bb, 0, c);
        }

        private CMDate ReadCMDate(CMBinaryReader br)
        {
            return new CMDate
            {
                Day = br.ReadInt16(),
                Year = br.ReadInt16(),
                IsLeapYear = br.ReadInt32(),
            };
        }

        private DateTime? ReadDateTime(CMBinaryReader br)
        {
            return GetDateFromCMDate(ReadCMDate(br));
        }

        private DateTime? GetDateFromCMDate(CMDate cmDate)
        {
            if (cmDate.Day == 0 && cmDate.Year == 0 /*&& cmDate.IsLeapYear == 0*/)
            {
                return null;
            }
            else
            {
                return new DateTime(cmDate.Year, 1, 1).AddDays(cmDate.Day);
            }
        }

        private CMName GetNameFromId(int id, List<CMName> a)
        {
            return id < 0 ? null : a[id];
        }

        private CMNation GetNationFromId(int id, List<CMNation> a)
        {
            return id < 0 ? null : a[id];
        }

        private CMDivision GetDivisionFromId(int id, List<CMDivision> a)
        {
            return id < 0 ? null : a[id];
        }

        private CMClub GetClubFromId(int id, List<CMClub> a)
        {
            return id < 0 ? null : a[id];
        }

        private CMPlayer GetPlayerFromId(int id, List<CMPlayer> a)
        {
            return id < 0 ? null : a[id];
        }
    }
}
