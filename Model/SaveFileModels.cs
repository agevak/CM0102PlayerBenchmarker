using System;

namespace CM.Model
{
    public class Side
    {
        public String Name { get; }
        public String Code { get; }

        public readonly Func<CMStaff, Boolean> IsSuitableFor;

        public Side(String name, String code, Func<CMStaff, Boolean> isSuitableFor)
        {
            Name = name;
            Code = code;
            IsSuitableFor = isSuitableFor;
        }
    }

    public class Position
    {
        public String Name { get; }
        public String Code { get; }

        public readonly Func<CMStaff, Boolean> IsSuitableFor;

        public Position(String name, String code, Func<CMStaff, Boolean> isSuitableFor)
        {
            Name = name;
            Code = code;
            IsSuitableFor = isSuitableFor;
        }
    }

    public class RatingPosition
    {
        public String Name { get; }
        public String Code { get; }

        public readonly Func<CMStaff, Boolean> IsSuitableFor;

        public RatingPosition(String name, String code, Func<CMStaff, Boolean> isSuitableFor)
        {
            Name = name;
            Code = code;
            IsSuitableFor = isSuitableFor;
        }
    }

    public class ContractType
    {
        public Int32 Id { get; }
        public String Name { get; }

        public readonly Func<Byte, Boolean> IsSuitableFor;

        public ContractType(Int32 id, String name, Func<Byte, Boolean> isSuitableFor)
        {
            Id = id;
            Name = name;
            IsSuitableFor = isSuitableFor;
        }
    }

    public class SquadStatus
    {
        public Int32 Id { get; }
        public String Name { get; }

        public readonly Func<Byte, Boolean> IsSuitableFor;

        public SquadStatus(Int32 id, String name, Func<Byte, Boolean> isSuitableFor)
        {
            Id = id;
            Name = name;
            IsSuitableFor = isSuitableFor;
        }
    }

    public class TransferStatus
    {
        public Int32 Id { get; }
        public String Name { get; }

        public readonly Func<Byte, Boolean> IsSuitableFor;

        public TransferStatus(Int32 id, String name, Func<Byte, Boolean> isSuitableFor)
        {
            Id = id;
            Name = name;
            IsSuitableFor = isSuitableFor;
        }
    }

    public class CMBlock
    {
        public Int32 Position;
        public Int32 Size;
        public String Name;
    }
    public class CMDate
    {
        public Int16 Day;
        public Int16 Year;
        public Int32 IsLeapYear;
    }

    public class CMNation
    {
        public Int32 Id;
        public String Name;
        public SByte GenderName;
        public String ShortName;
        public SByte GenderShortName;
        public String Code;
        public String Nationality;
        public Int32 ContinentId;
        public SByte Region;
        public SByte ActualRegion;
        public SByte FirstLanguage;
        public SByte SecondLanguage;
        public SByte ThirdLanguage;
        public Int32 CapitalId;
        public SByte StateOfDevelopment;
        public SByte GroupMembership;
        public Int32 NationalStadiumId;
        public SByte GameImportance;
        public SByte LeagueStandard;
        public Int16 NumberClubs;
        public Int32 NumberStaff;
        public Int16 UpdateDay;
        public Int16 Reputation;
        public Int32 ForeColour1Id;
        public Int32 BackColour1Id;
        public Int32 ForeColour2Id;
        public Int32 BackColour2Id;
        public Int32 ForeColour3Id;
        public Int32 BackColour3Id;
        public Double FIFACoefficient;
        public Double FIFACoefficient91;
        public Double FIFACoefficient92;
        public Double FIFACoefficient93;
        public Double FIFACoefficient94;
        public Double FIFACoefficient95;
        public Double FIFACoefficient96;
        public Double UEFACoefficient91;
        public Double UEFACoefficient92;
        public Double UEFACoefficient93;
        public Double UEFACoefficient94;
        public Double UEFACoefficient95;
        public Double UEFACoefficient96;
        public Int32 RivalNation1Id;
        public Int32 RivalNation2Id;
        public Int32 RivalNation3Id;
        public SByte LeagueSelected;
        public Int32 ShortlistOffset;
        public SByte GamesPlayed;
    }

    public class CMDivision
    {
        public Int32 Id;
        public String Name;
        public SByte GenderName;
        public String ShortName;
        public SByte GenderShortName;
        public String Code;
        public SByte Scope;
        public SByte Selected;
        public Int32 ContinentId;
        public Int32 NationId;
        public Int32 ForeColourId;
        public Int32 BackColourId;
        public Int16 Reputation;

        public CMNation Nation;
    }

    public class CMClub
    {
        public Int32 Id;
        public String Name;
        public SByte GenderName;
        public String ShortName;
        public SByte GenderShortName;
        public Int32 NationId;
        public Int32 DivisionId;
        public Int32 LastDivisionId;
        public SByte LastPosition;
        public Int32 ReserveDivisionId;
        public SByte ProfessionalStatus;
        public Int32 Cash;
        public Int32 StadiumId;
        public SByte OwnStadium;
        public Int32 ReserveStadiumId;
        public SByte MatchDay;
        public Int32 Attendance;
        public Int32 MinAttendance;
        public Int32 MaxAttendance;
        public SByte Training;
        public Int16 Reputation;
        public SByte PLC;
        public Int32 ForeColour1Id;
        public Int32 BackColour1Id;
        public Int32 ForeColour2Id;
        public Int32 BackColour2Id;
        public Int32 ForeColour3Id;
        public Int32 BackColour3Id;
        public Int32 FavStaff1Id;
        public Int32 FavStaff2Id;
        public Int32 FavStaff3Id;
        public Int32 DisStaff1Id;
        public Int32 DisStaff2Id;
        public Int32 DisStaff3Id;
        public Int32 Rival1Id;
        public Int32 Rival2Id;
        public Int32 Rival3Id;
        public Int32 ChairmanId;
        public Int32[] DirectorsIds;
        public Int32 ManagerId;
        public Int32 AssistantManagerId;
        public Int32[] SquadIds;
        public Int32[] CoachIds;
        public Int32[] ScoutIds;
        public Int32[] PhysioIds;
        public Int32 EuroFlag;
        public SByte EuroSeeding;
        public Int32[] TeamSelectedIds;
        public Int32[] TacticTrainingIds;
        public Int32 TacticSelected;
        public SByte HasLinkedClub;

        public CMNation Nation;
    }

    public class CMName
    {
        public String Name;
        public Int32 Id;
        public Int32 NationId;
        public SByte Count;
    }

    public class CMContract
    {
        public Int32 Id;
        public Int32 ClubId;
        public Byte[] Unknown1;
        public Int32 Wage;
        public Int32 GoalBonus;
        public Int32 AssistBonus;
        public Int32 CleanSheetBonus;
        public Byte NonPromotionRC;
        public Byte MinimumFeeRC;
        public Byte NonPlayingRC;
        public Byte RelegationRC;
        public Byte ManagerJobRC;
        public Int32 ReleaseFee;
        public DateTime? DateStarted;
        public DateTime? DateEnded;
        public Byte Type;
        public Byte[] Unknown2;
        public Boolean LeavingOnBosman;
        public Int32 TransferArrangedForClubId;
        public Byte TransferStatus;
        public Byte SquadStatus;

        public CMClub Club;
        public CMClub TransferArrangedForClub;
        public String UnprotectedReason;
        public Boolean HasNonPromotionRC;
        public Boolean IsNonPromotionRCActive;
        public Boolean HasMinimumFeeRC;
        public Boolean IsMinimumFeeRCActive;
        public Boolean HasNonPlayingRC;
        public Boolean IsNonPlayingRCActive;
        public Boolean HasRelegationRC;
        public Boolean IsRelegationRCActive;
        public Boolean HasManagerJobRC;
        public Boolean IsManagerJobRCActive;
    }

    public class CMPlayer
    {
        public Int32 Id;
        public Byte SquardNumber;
        public Int16 CurrentAbility;
        public Int16 PotentialAbility;
        public Int16 HomeReputation;
        public Int16 CurrentReputation;
        public Int16 WorldReputation;
        public SByte Goalkeeper;
        public SByte Sweeper;
        public SByte Defender;
        public SByte DefensiveMidfielder;
        public SByte Midfielder;
        public SByte AttackingMidfielder;
        public SByte Attacker;
        public SByte WingBack;
        public SByte RightSide;
        public SByte LeftSide;
        public SByte CentreSide;
        public SByte FreeRole;
        public SByte Acceleration;
        public SByte Aggression;
        public SByte Agility;
        public SByte Anticipation;
        public SByte Balance;
        public SByte Bravery;
        public SByte Consistency;
        public SByte Corners;
        public SByte Crossing;
        public SByte Decisions;
        public SByte Dirtiness;
        public SByte Dribbling;
        public SByte Finishing;
        public SByte Flair;
        public SByte SetPieces;
        public SByte Handling;
        public SByte Heading;
        public SByte ImportantMatches;
        public SByte InjuryProneness;
        public SByte Jumping;
        public SByte Influence;
        public SByte LeftFoot;
        public SByte LongShots;
        public SByte Marking;
        public SByte OffTheBall;
        public SByte NaturalFitness;
        public SByte OneOnOnes;
        public SByte Pace;
        public SByte Passing;
        public SByte Penalties;
        public SByte Positioning;
        public SByte Reflexes;
        public SByte RightFoot;
        public SByte Stamina;
        public SByte Strength;
        public SByte Tackling;
        public SByte Teamwork;
        public SByte Technique;
        public SByte ThrowIns;
        public SByte Versatility;
        public SByte Creativity;
        public SByte WorkRate;
        public SByte Morale;
    }

    public class CMStaff
    {
        public Int32 Id;
        public Int32 FirstNameId;
        public Int32 SecondNameId;
        public Int32 CommonNameId;
        public DateTime? DateOfBirth;
        public Int16 YearOfBirth;
        public Int32 FirstNationId;
        public Int32 SecondNationId;
        public Byte IntApps;
        public Byte IntGoals;
        public Int32 NationalJobId;
        public SByte JobForNation;
        public DateTime? DateJoinedNation;
        public DateTime? DateExpiresNation;
        public Int32 ClubJobId;
        public SByte JobForClub;
        public DateTime? DateJoinedClub;
        public DateTime? DateExpiresClub;
        public Int32 Wage;
        public Int32 Value;
        public SByte Adaptability;
        public SByte Ambition;
        public SByte Determination;
        public SByte Loyality;
        public SByte Pressure;
        public SByte Professionalism;
        public SByte Sportsmanship;
        public SByte Temperament;
        public SByte PlayingSquad;
        public SByte Classification;
        public SByte ClubValuation;
        public Int32 PlayerId;
        public Int32 StaffPreferencesId;
        public Int32 NonPlayerId;
        public SByte SquadSelectedFor;

        public CMName FirstName;
        public CMName SecondName;
        public CMName CommonName;
        public CMNation FirstNation;
        public CMNation SecondNation;
        public CMClub ClubJob;
        public CMPlayer Player;
        public Int32? Age;
        public CMContract Contract;
        public CMContract LoanContract;

        public Double[] Ratings;

        public Boolean IsFavorite;
    }
}
