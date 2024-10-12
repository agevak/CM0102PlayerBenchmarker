using System;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Reflection;

namespace CM.Model
{
    public class CMAttribute
    {
        public static readonly CMAttribute[] LIST = new CMAttribute[]
        {
            new CMAttribute("Acceleration", null, AttributeRangeType._1_20), // 0
            new CMAttribute("Aggression", null, AttributeRangeType._1_20), // 1
            new CMAttribute("Agility", null, AttributeRangeType._1_20), // 2
            new CMAttribute("Anticipation", null, AttributeRangeType._sbyte), // 3
            new CMAttribute("Balance", null, AttributeRangeType._1_20), // 4
            new CMAttribute("Bravery", null, AttributeRangeType._1_20), // 5
            new CMAttribute("Creativity", null, AttributeRangeType._sbyte), // 6
            new CMAttribute("Crossing",          null, AttributeRangeType._sbyte), // 7
            new CMAttribute("Decisions",         null, AttributeRangeType._sbyte), // 8
            new CMAttribute("Determination", null, AttributeRangeType._1_20), // 9 
            new CMAttribute("Dribbling",         null, AttributeRangeType._sbyte), // 10
            new CMAttribute("Finishing",         null, AttributeRangeType._sbyte), // 11
            new CMAttribute("Flair",             null, AttributeRangeType._1_20), // 12
            new CMAttribute("Handling",          null, AttributeRangeType._sbyte), // 13
            new CMAttribute("Heading",           null, AttributeRangeType._sbyte), // 14
            new CMAttribute("Influence",         null, AttributeRangeType._1_20), // 15
            new CMAttribute("Jumping",           null, AttributeRangeType._1_20), // 16
            new CMAttribute("Long Shots",        null, AttributeRangeType._sbyte), // 17
            new CMAttribute("Marking",           null, AttributeRangeType._sbyte), // 18
            new CMAttribute("Off The Ball",      null, AttributeRangeType._sbyte), // 19
            new CMAttribute("Pace", null, AttributeRangeType._1_20), // 20
            new CMAttribute("Passing",           null, AttributeRangeType._sbyte), // 21
            new CMAttribute("Positioning",       null, AttributeRangeType._sbyte), // 22
            new CMAttribute("Reflexes",          null, AttributeRangeType._sbyte), // 23
            new CMAttribute("Set Pieces", null, AttributeRangeType._1_20), // 24
            new CMAttribute("Stamina", null, AttributeRangeType._1_20), // 25
            new CMAttribute("Strength", null, AttributeRangeType._1_20), // 26
            new CMAttribute("Tackling",          null, AttributeRangeType._sbyte), // 27
            new CMAttribute("Teamwork", null, AttributeRangeType._1_20), // 28
            new CMAttribute("Technique", null, AttributeRangeType._1_20), // 29
            new CMAttribute("Work Rate", null, AttributeRangeType._1_20), // 30
            new CMAttribute("Adaptability", null, AttributeRangeType._1_20), // 31
            new CMAttribute("Important Matches", null, AttributeRangeType._1_20), // 32
            new CMAttribute("Consistency", null, AttributeRangeType._1_20), // 33
            new CMAttribute("Corners", null, AttributeRangeType._1_20), // 34
            new CMAttribute("Dirtiness", null, AttributeRangeType._1_20), // 35
            new CMAttribute("Natural Fitness",   null, AttributeRangeType._1_20), // 36
            new CMAttribute("Injury Proneness", null, AttributeRangeType._1_20), // 37
            new CMAttribute("Loyality",          null, AttributeRangeType._1_20), // 38
            new CMAttribute("One On Ones",       null, AttributeRangeType._sbyte), // 39
            new CMAttribute("Penalties",         null, AttributeRangeType._sbyte), // 40
            new CMAttribute("Versatility",       null, AttributeRangeType._1_20), // 41
            new CMAttribute("Throw Ins",         null, AttributeRangeType._sbyte), // 42
            new CMAttribute("Ambition",          null, AttributeRangeType._1_20), // 43
            new CMAttribute("Pressure",          null, AttributeRangeType._1_20), // 44
            new CMAttribute("Professionalism", null, AttributeRangeType._1_20), // 45
            new CMAttribute("Sportsmanship", null, AttributeRangeType._1_20), // 46
            new CMAttribute("Temperament", null, AttributeRangeType._1_20), // 47
            new CMAttribute("Current Ability", null, AttributeRangeType._1_200), // 48
            new CMAttribute("Potential Ability", null, AttributeRangeType._1_200), // 49
        }.OrderBy(x => (x.FieldName == "CurrentAbility") ? "ZZY" : ((x.FieldName == "PotentialAbility") ? "ZZZ" : x.FieldName)).ToArray();

        public string DisplayName { get; }
        public string FieldName { get; }
        public int Min { get; set; } = 1;
        public int Max { get; set; } = 20;

        public CMAttribute(string displayName, string fieldName, AttributeRangeType rangeType)
        {
            if (fieldName == null)
            {
                fieldName = "";
                for (int i = 0; i < displayName.Length; i++)
                {
                    if (displayName[i] == ' ') continue;
                    char ch = displayName[i];
                    if (i > 0 && displayName[i - 1] == ' ') ch = char.ToUpper(ch);
                    fieldName += ch;
                }
            }

            DisplayName = displayName;
            FieldName = fieldName;
            Min = 1;
            Max = 20;
            if (rangeType == AttributeRangeType._sbyte)
            {
                Min = -128;
                Max = 127;
            }
            else if (rangeType == AttributeRangeType._1_200) Max = 200;
        }

        public int GetValue(CMStaff staff)
        {
            FieldInfo field = staff.Player.GetType().GetField(FieldName);
            if (field != null) return Convert.ToInt32(field.GetValue(staff.Player));
            field = staff.GetType().GetField(FieldName);
            return Convert.ToInt32(field.GetValue(staff));
        }

        public void SetValue(CMStaff staff, int value)
        {
            FieldInfo field = staff.Player.GetType().GetField(FieldName);
            object obj = staff.Player;
            if (field == null)
            {
                field = staff.GetType().GetField(FieldName);
                obj = staff;
            }
            if (field.FieldType == typeof(SByte)) field.SetValue(obj, Convert.ToSByte(value));
            else if (field.FieldType == typeof(Int16)) field.SetValue(obj, Convert.ToInt16(value));
            else throw new NotImplementedException();
        }
    }

    public enum AttributeRangeType
    {
        _1_20,
        _sbyte,
        _1_200
    }
}
