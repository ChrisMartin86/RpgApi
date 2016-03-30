using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RpgApi.Models.NPCGeneratorModels.DnD.FifthEdition
{
    public class NPC
    {
        private NPCTemplate NPCTemplate_ { get; set; }
        private NPCRace NPCRace_ { get; set; }

        public string NPCTemplate { get { return NPCTemplate_.ToString(); } }
        public string NPCRace { get { return NPCRace_.ToString(); } }
        public int ArmorClass { get; set; }
        public int HitPoints { get; set; }
        public int SpeedInFeet { get; set; }
        public int ProficiencyBonus { get; set; }

        public int Strength { get; set; }
        public int Dexterity { get; set; }
        public int Constitution { get; set; }
        public int Intelligence { get; set; }
        public int Wisdom { get; set; }
        public int Charisma { get; set; }
        public IEnumerable<string> Languages { get; set; }
        public IDictionary<string, int> Skills { get; set; }
        public IEnumerable<string> Actions { get; set; }

        public IEnumerable<string> Spells { get; set; }

        public NPC(NPCTemplate baseTemplate = FifthEdition.NPCTemplate.Random, NPCRace baseRace = FifthEdition.NPCRace.Random)
        {

            if ((int)baseTemplate > 4)
                throw new IndexOutOfRangeException("Invalid template number");

            if ((int)baseRace > 9)
                throw new IndexOutOfRangeException("Invalid race number");

            if (FifthEdition.NPCTemplate.Random == baseTemplate)
                assignRandomTemplate();
            else
                NPCTemplate_ = baseTemplate;

            if (FifthEdition.NPCRace.Random == baseRace)
                assignRandomRace();
            else
                NPCRace_ = baseRace;

            initializeTemplateFields();

            IEnumerable<int> statList = null;

            if (baseTemplate == FifthEdition.NPCTemplate.Commoner || baseTemplate == FifthEdition.NPCTemplate.Merchant)
                statList = rollStats();
            else
                statList = rollStats(true);

            assignStats(statList);

            applyRacialStatModifiers();

        }

        private void initializeTemplateFields()
        { 
            ArmorClass = 0;
            HitPoints = 0;
            SpeedInFeet = 0;
            ProficiencyBonus = 0;
            Strength = 0;
            Dexterity = 0;
            Constitution = 0;
            Intelligence = 0;
            Wisdom = 0;
            Charisma = 0;

            Languages = new string[0];
            Skills = new Dictionary<string, int>();
            Actions = new string[0];
            Spells = new string[0];
        }

        private IEnumerable<int> rollStats(bool heroic = false)
        {
            var statList = new List<int>();

            for (int i = 0; i < 6; i++)
            {
                var diceDict = new Dictionary<int, int>();

                if (heroic)
                    diceDict.Add(6, 4);
                else
                    diceDict.Add(6, 3);

                var diceRoller = new DiceRollerModels.DiceRoller(diceDict);

                var results = diceRoller.CalculateRoll();

                foreach (var result in results)
                {
                    List<int> individualResults = (List<int>)result.IndividualResults;

                    if (heroic)
                        statList.Add(individualResults.Sum() - individualResults.Min());
                    else
                        statList.Add(individualResults.Sum());
                }
            }

            return statList;

        }

        private void assignStats(IEnumerable<int> unmodifiedStatList)
        {
            if (unmodifiedStatList.Count() != 6)
                throw new ArgumentException("Wrong number of values in list.", "unmodifiedStatList");

            int[] randomStatArray = unmodifiedStatList.ToArray();

            int[] sortedCopy = randomStatArray.OrderBy(i => i).ToArray();

            switch (NPCTemplate_)
            {
                case FifthEdition.NPCTemplate.Random:
                    break;
                case FifthEdition.NPCTemplate.Commoner:
                    {
                        Strength = randomStatArray[0];
                        Dexterity = randomStatArray[1];
                        Constitution = randomStatArray[2];
                        Intelligence = randomStatArray[3];
                        Wisdom = randomStatArray[4];
                        Charisma = randomStatArray[5];
                        break;
                    }
                case FifthEdition.NPCTemplate.Merchant:
                    {
                        Strength = sortedCopy[0];
                        Dexterity = sortedCopy[2];
                        Constitution = sortedCopy[1];
                        Intelligence = sortedCopy[4];
                        Wisdom = sortedCopy[3];
                        Charisma = sortedCopy[5];
                        break;
                    }
                case FifthEdition.NPCTemplate.Warrior:
                    {
                        Strength = sortedCopy[5];
                        Dexterity = sortedCopy[3];
                        Constitution = sortedCopy[4];
                        Intelligence = sortedCopy[0];
                        Wisdom = sortedCopy[2];
                        Charisma = sortedCopy[1];
                        break;
                    }
                case FifthEdition.NPCTemplate.Wizard:
                    {
                        Strength = sortedCopy[0];
                        Dexterity = sortedCopy[2];
                        Constitution = sortedCopy[1];
                        Intelligence = sortedCopy[5];
                        Wisdom = sortedCopy[4];
                        Charisma = sortedCopy[3];
                        break;
                    }
                default:
                    break;
            }


        }

        private void applyRacialStatModifiers()
        {
            switch (NPCRace_)
            {
                case FifthEdition.NPCRace.Random:
                    break;
                case FifthEdition.NPCRace.Human:
                    {
                        Strength++;
                        Dexterity++;
                        Constitution++;
                        Intelligence++;
                        Wisdom++;
                        Charisma++;
                        break;
                    }
                case FifthEdition.NPCRace.Elf:
                    {
                        Dexterity = Dexterity + 2;
                        break;
                    }
                case FifthEdition.NPCRace.Dwarf:
                    {
                        Constitution = Constitution + 2;
                        break;
                    }
                case FifthEdition.NPCRace.Halfling:
                    {
                        Dexterity = Dexterity + 2;
                        break;
                    }
                case FifthEdition.NPCRace.HalfOrc:
                    {
                        Strength = Strength + 2;
                        Constitution++;
                        break;
                    }
                case FifthEdition.NPCRace.HalfElf:
                    {
                        Charisma = Charisma + 2;

                        switch (NPCTemplate_)
                        {
                            case FifthEdition.NPCTemplate.Random:
                                break;
                            case FifthEdition.NPCTemplate.Commoner:
                                {
                                    Strength++;
                                    Wisdom++;
                                    break;
                                }
                            case FifthEdition.NPCTemplate.Merchant:
                                {
                                    Charisma++;
                                    Intelligence++;
                                    break;
                                }
                            case FifthEdition.NPCTemplate.Warrior:
                                {
                                    Strength++;
                                    Constitution++;
                                    break;
                                }
                            case FifthEdition.NPCTemplate.Wizard:
                                {
                                    Intelligence++;
                                    Wisdom++;

                                    break;
                                }
                            default:
                                break;
                        }

                        break;
                    }
                case FifthEdition.NPCRace.DragonBorn:
                    {
                        Strength = Strength + 2;
                        Charisma++;
                        break;
                    }
                case FifthEdition.NPCRace.Gnome:
                    {
                        Intelligence = Intelligence + 2;
                        break;
                    }
                case FifthEdition.NPCRace.Tiefling:
                    {
                        Intelligence++;
                        Charisma = Charisma + 2;
                        break;
                    }
                default:
                    break;
            }
        }

        private void assignRandomTemplate()
        {
            NPCTemplate_ = (NPCTemplate)DiceRollerModels.DiceRoller.RollDice(4);
        }

        private void assignRandomRace()
        {
            NPCRace_ = (NPCRace)(DiceRollerModels.DiceRoller.RollDice(8));
        }
    }

    public enum NPCTemplate
    {
        Random = 0,
        Commoner = 1,
        Merchant = 2,
        Warrior = 3,
        Wizard = 4
    }

    public enum NPCRace
    {
        Random = 0,
        Human = 1,
        Elf = 2,
        Dwarf = 3,
        Halfling = 4,
        HalfOrc = 5,
        HalfElf = 6,
        DragonBorn = 7,
        Gnome = 8,
        Tiefling = 9,
    }


}