﻿using Kingmaker.Blueprints;
using Kingmaker.Blueprints.Classes;
using Kingmaker.Blueprints.Classes.Prerequisites;
using Kingmaker.Blueprints.Classes.Selection;
using Kingmaker.Blueprints.Classes.Spells;
using Kingmaker.Designers.Mechanics.Facts;
using Kingmaker.ElementsSystem;
using Kingmaker.EntitySystem.Stats;
using Kingmaker.Enums;
using Kingmaker.RuleSystem;
using Kingmaker.UnitLogic;
using Kingmaker.UnitLogic.Abilities.Blueprints;
using Kingmaker.UnitLogic.Abilities.Components;
using Kingmaker.UnitLogic.Buffs.Blueprints;
using Kingmaker.UnitLogic.FactLogic;
using Kingmaker.UnitLogic.Mechanics;
using Kingmaker.UnitLogic.Mechanics.Actions;
using Kingmaker.UnitLogic.Mechanics.Components;
using TabletopTweaks.Config;
using TabletopTweaks.Extensions;
using TabletopTweaks.NewComponents;
using TabletopTweaks.Utilities;

namespace TabletopTweaks.NewContent.Bloodlines {
    class DestinedBloodline {

        static BlueprintFeatureReference BloodlineRequisiteFeature = ResourcesLibrary.TryGetBlueprint<BlueprintFeature>(
            Settings.Blueprints.NewBlueprints["BloodlineRequisiteFeature"]).ToReference<BlueprintFeatureReference>();
        static BlueprintFeatureReference DestinedBloodlineRequisiteFeature = CreateBloodlineRequisiteFeature();

        static BlueprintFeatureReference CreateBloodlineRequisiteFeature() {
            var AberrantBloodlineRequisiteFeature = Helpers.Create<BlueprintFeature>(bp => {
                bp.m_AssetGuid = Settings.Blueprints.NewBlueprints["DestinedBloodlineRequisiteFeature"];
                bp.IsClassFeature = true;
                bp.HideInUI = true;
                bp.Ranks = 1;
                bp.HideInCharacterSheetAndLevelUp = true;
                bp.name = "DestinedBloodlineRequisiteFeature";
                bp.SetName("Destined Bloodline");
                bp.SetDescription("Destined Bloodline Requisite Feature");
            });
            Resources.AddBlueprint(AberrantBloodlineRequisiteFeature);
            return AberrantBloodlineRequisiteFeature.ToReference<BlueprintFeatureReference>();
        }
        public static void AddBloodragerDestinedBloodline() {
            var BloodragerStandardRageBuff = ResourcesLibrary.TryGetBlueprint<BlueprintBuff>("5eac31e457999334b98f98b60fc73b2f");
            var BloodragerClass = ResourcesLibrary.TryGetBlueprint<BlueprintCharacterClass>("d77e67a814d686842802c9cfd8ef8499").ToReference<BlueprintCharacterClassReference>();
            var GreenragerArchetype = ResourcesLibrary.TryGetBlueprint<BlueprintArchetype>("5648585af75596f4a9fa3ae385127f57").ToReference<BlueprintArchetypeReference>();
            //Used Assets
            var TrueStrike = ResourcesLibrary.TryGetBlueprint<BlueprintAbility>("2c38da66e5a599347ac95b3294acbe00");
            var LuckDomain = ResourcesLibrary.TryGetBlueprint<BlueprintAbility>("9af0b584f6f754045a0a79293d100ab3"); 
            //Bonus Spells
            var MageShield = ResourcesLibrary.TryGetBlueprint<BlueprintAbility>("ef768022b0785eb43a18969903c537c4").ToReference<BlueprintAbilityReference>();
            var Blur = ResourcesLibrary.TryGetBlueprint<BlueprintAbility>("14ec7a4e52e90fa47a4c8d63c69fd5c1").ToReference<BlueprintAbilityReference>();
            var ProtectionFromEnergy = ResourcesLibrary.TryGetBlueprint<BlueprintAbility>("d2f116cfe05fcdd4a94e80143b67046f").ToReference<BlueprintAbilityReference>();
            var FreedomOfMovement = ResourcesLibrary.TryGetBlueprint<BlueprintAbility>("4c349361d720e844e846ad8c19959b1e").ToReference<BlueprintAbilityReference>();
            //Bonus Feats
            var Diehard = ResourcesLibrary.TryGetBlueprint<BlueprintFeature>("86669ce8759f9d7478565db69b8c19ad").ToReference<BlueprintFeatureReference>();
            var Endurance = ResourcesLibrary.TryGetBlueprint<BlueprintFeature>("54ee847996c25cd4ba8773d7b8555174").ToReference<BlueprintFeatureReference>();
            var ImprovedInitiative = ResourcesLibrary.TryGetBlueprint<BlueprintFeature>("797f25d709f559546b29e7bcb181cc74").ToReference<BlueprintFeatureReference>();
            var IntimidatingProwess = ResourcesLibrary.TryGetBlueprint<BlueprintFeature>("d76497bfc48516e45a0831628f767a0f").ToReference<BlueprintFeatureReference>();
            var SiezeTheMoment = ResourcesLibrary.TryGetBlueprint<BlueprintFeature>("1191ef3065e6f8e4f9fbe1b7e3c0f760").ToReference<BlueprintFeatureReference>();
            var LightningReflexes = ResourcesLibrary.TryGetBlueprint<BlueprintFeature>("15e7da6645a7f3d41bdad7c8c4b9de1e").ToReference<BlueprintFeatureReference>();
            var WeaponFocus = ResourcesLibrary.TryGetBlueprint<BlueprintFeature>("1e1f627d26ad36f43bbd26cc2bf8ac7e").ToReference<BlueprintFeatureReference>();
            //Bloodline Powers
            var BloodragerDestinedStrikeResource = Helpers.Create<BlueprintAbilityResource>(bp => {
                bp.m_AssetGuid = Settings.Blueprints.NewBlueprints["BloodragerDestinedStrikeResource"];
                bp.name = "BloodragerDestinedStrikeResource";
                bp.m_Min = 0;
                bp.m_MaxAmount = new BlueprintAbilityResource.Amount {
                    BaseValue = 3,
                    IncreasedByStat = false,
                    m_Class = new BlueprintCharacterClassReference[0],
                    m_ClassDiv = new BlueprintCharacterClassReference[0],
                    m_Archetypes = new BlueprintArchetypeReference[0],
                    m_ArchetypesDiv = new BlueprintArchetypeReference[0]
                };
            });
            var BloodragerDestinedStrikeResourceIncrease = Helpers.Create<BlueprintFeature>(bp => {
                bp.m_AssetGuid = Settings.Blueprints.NewBlueprints["BloodragerDestinedStrikeResourceIncrease"];
                bp.name = "BloodragerDestinedStrikeResourceIncrease";
                bp.HideInUI = true;
                bp.AddComponent(Helpers.Create<IncreaseResourceAmount>(c => {
                    c.m_Resource = BloodragerDestinedStrikeResource.ToReference<BlueprintAbilityResourceReference>();
                    c.Value = 2;
                }));
            });
            var BloodragerDestinedStrikeBuff = Helpers.Create<BlueprintBuff>(bp => {
                bp.m_AssetGuid = Settings.Blueprints.NewBlueprints["BloodragerDestinedStrikeBuff"];
                bp.name = "BloodragerDestinedStrikeBuff";
                bp.Stacking = StackingType.Rank;
                bp.Ranks = 5;
                bp.SetName("Destined Strike");
                bp.SetDescription("You can grant yourself an insight bonus equal to 1/2 your bloodrager level (minimum 1) on one melee attack.");
                bp.m_Icon = TrueStrike.Icon;
                bp.AddComponent(Helpers.Create<AddContextStatBonus>(c => {
                    c.Descriptor = ModifierDescriptor.Insight;
                    c.Stat = StatType.AdditionalAttackBonus;
                    c.Value = new ContextValue {
                        ValueType = ContextValueType.Rank,
                        ValueRank = AbilityRankType.StatBonus
                    };
                }));
                bp.AddComponent(Helpers.Create<RemoveBuffRankOnAttack>());
                bp.AddComponent(Helpers.Create<ContextRankConfig>(c => {
                    c.m_Type = AbilityRankType.StatBonus;
                    c.m_BaseValueType = ContextRankBaseValueType.ClassLevel;
                    c.m_Progression = ContextRankProgression.Div2;
                    c.m_StartLevel = 1;
                    c.m_StepLevel = 2;
                    c.m_Max = 20;
                    c.m_Min = 1;
                    c.m_UseMin = true;
                    c.m_Class = new BlueprintCharacterClassReference[] { BloodragerClass };
                }));
            });
            var BloodragerDestinedStrikeAbility = Helpers.Create<BlueprintAbility>(bp => {
                bp.m_AssetGuid = Settings.Blueprints.NewBlueprints["BloodragerDestinedAbility"];
                bp.name = "BloodragerDestinedAbility";
                bp.SetName("Destined Strike");
                bp.SetDescription("At 1st level, as a free action up to three times per day you can grant yourself an insight bonus equal to 1/2 your "
                    + "bloodrager level (minimum 1) on one melee attack. At 12th level, you can use this ability up to five times per day.");
                bp.LocalizedDuration = new Kingmaker.Localization.LocalizedString();
                bp.LocalizedSavingThrow = new Kingmaker.Localization.LocalizedString();
                bp.Range = AbilityRange.Personal;
                bp.EffectOnAlly = AbilityEffectOnUnit.Harmful;
                bp.Animation = Kingmaker.Visual.Animation.Kingmaker.Actions.UnitAnimationActionCastSpell.CastAnimationStyle.Immediate;
                bp.ActionType = Kingmaker.UnitLogic.Commands.Base.UnitCommand.CommandType.Free;
                bp.m_Icon = TrueStrike.Icon;
                bp.ResourceAssetIds = TrueStrike.ResourceAssetIds;
                bp.AddComponent(Helpers.Create<AbilityResourceLogic>(c => {
                    c.m_RequiredResource = BloodragerDestinedStrikeResource.ToReference<BlueprintAbilityResourceReference>();
                    c.m_IsSpendResource = true;
                    c.Amount = 1;
                }));
                var addInsightBonus = Helpers.Create<ContextActionApplyBuff>(c => {
                    c.m_Buff = BloodragerDestinedStrikeBuff.ToReference<BlueprintBuffReference>();
                    c.IsNotDispelable = true;
                    c.Permanent = true;
                    c.DurationValue = new ContextDurationValue() {
                        Rate = DurationRate.Rounds,
                        DiceType = DiceType.Zero,
                        DiceCountValue = new ContextValue() {
                            ValueType = ContextValueType.Simple,
                            Value = 0
                        },
                        BonusValue = new ContextValue() {
                            ValueType = ContextValueType.Simple,
                            Value = 1
                        }
                    };
                });
                bp.AddComponent(Helpers.Create<AbilityEffectRunAction>(c => {
                    c.Actions = new ActionList();
                    c.Actions.Actions = new GameAction[] { addInsightBonus };
                }));
            });
            var BloodragerDestinedStrike = Helpers.Create<BlueprintFeature>(bp => {
                bp.m_AssetGuid = Settings.Blueprints.NewBlueprints["BloodragerDestinedStrike"];
                bp.name = "BloodragerDestinedStrike";
                bp.SetName(BloodragerDestinedStrikeAbility.Name);
                bp.SetDescription(BloodragerDestinedStrikeAbility.Description);
                bp.AddComponent(Helpers.Create<AddFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[] {
                        BloodragerDestinedStrikeAbility.ToReference<BlueprintUnitFactReference>(),
                    };
                }));
                bp.AddComponent(Helpers.Create<AddAbilityResources>(c => {
                    c.m_Resource = BloodragerDestinedStrikeResource.ToReference<BlueprintAbilityResourceReference>();
                    c.RestoreAmount = true;
                }));
                bp.IsClassFeature = true;
                bp.Ranks = 1;
                bp.m_Icon = BloodragerDestinedStrikeAbility.Icon;
            });
            var BloodragerDestinedFatedBloodrager = Helpers.Create<BlueprintFeature>(bp => {
                bp.m_AssetGuid = Settings.Blueprints.NewBlueprints["BloodragerDestinedFatedBloodrager"];
                bp.name = "BloodragerDestinedFatedBloodrager";
                bp.SetName("Fated Bloodrager");
                bp.SetDescription("At 4th level, you gain a +1 luck bonus to AC and on saving throws. At 8th level and every "
                    + "4 levels thereafter, this bonus increases by 1 (to a maximum of +5 at 20th level).");
                bp.Ranks = 5;
            });
            var BloodragerDestinedFatedBloodragerBuff = Helpers.Create<BlueprintBuff>(bp => {
                bp.m_AssetGuid = Settings.Blueprints.NewBlueprints["BloodragerDestinedFatedBloodragerBuff"];
                bp.name = "BloodragerDestinedFatedBloodragerBuff";
                bp.m_Flags = BlueprintBuff.Flags.HiddenInUi;
                bp.SetName(BloodragerDestinedFatedBloodrager.Name);
                bp.SetDescription(BloodragerDestinedFatedBloodrager.Description);
                bp.AddComponent(Helpers.Create<AddContextStatBonus>(c => {
                    c.Descriptor = ModifierDescriptor.Luck;
                    c.Stat = StatType.AC;
                    c.Multiplier = 1;
                    c.Value = new ContextValue() {
                        ValueType = ContextValueType.Rank,
                        ValueRank = AbilityRankType.StatBonus
                    };
                }));
                bp.AddComponent(Helpers.Create<AddContextStatBonus>(c => {
                    c.Descriptor = ModifierDescriptor.Luck;
                    c.Stat = StatType.SaveFortitude;
                    c.Multiplier = 1;
                    c.Value = new ContextValue() {
                        ValueType = ContextValueType.Rank,
                        ValueRank = AbilityRankType.StatBonus
                    };
                }));
                bp.AddComponent(Helpers.Create<AddContextStatBonus>(c => {
                    c.Descriptor = ModifierDescriptor.Luck;
                    c.Stat = StatType.SaveReflex;
                    c.Multiplier = 1;
                    c.Value = new ContextValue() {
                        ValueType = ContextValueType.Rank,
                        ValueRank = AbilityRankType.StatBonus
                    };
                }));
                bp.AddComponent(Helpers.Create<AddContextStatBonus>(c => {
                    c.Descriptor = ModifierDescriptor.Luck;
                    c.Stat = StatType.SaveWill;
                    c.Multiplier = 1;
                    c.Value = new ContextValue() {
                        ValueType = ContextValueType.Rank,
                        ValueRank = AbilityRankType.StatBonus
                    };
                }));
                bp.AddComponent(Helpers.Create<ContextRankConfig>(c => {
                    c.m_BaseValueType = ContextRankBaseValueType.FeatureRank;
                    c.m_Type = AbilityRankType.StatBonus;
                    c.m_Feature = BloodragerDestinedFatedBloodrager.ToReference<BlueprintFeatureReference>();
                    c.m_Progression = ContextRankProgression.AsIs;
                }));
            });
            var BloodragerDestinedCertainStrike = Helpers.Create<BlueprintFeature>(bp => {
                bp.m_AssetGuid = Settings.Blueprints.NewBlueprints["BloodragerDestinedCertainStrike"];
                bp.name = "BloodragerDestinedCertainStrike";
                bp.SetName("Certain Strike");
                bp.SetDescription("At 8th level, you may reroll an attack roll once during a bloodrage. You must decide to use this ability after "
                    + "the die is rolled, but before the GM reveals the results. You must take the second result, even if it’s worse.");
            });
            var BloodragerDestinedCertainStrikeBuff = Helpers.Create<BlueprintBuff>(bp => {
                bp.m_AssetGuid = Settings.Blueprints.NewBlueprints["BloodragerDestinedCertainStrikeBuff"];
                bp.name = "BloodragerDestinedCertainStrikeBuff";
                //bp.m_Flags = BlueprintBuff.Flags.HiddenInUi;
                bp.m_Icon = LuckDomain.Icon;
                bp.SetName(BloodragerDestinedCertainStrike.Name);
                bp.SetDescription(BloodragerDestinedCertainStrike.Description);
                bp.AddComponent(Helpers.Create<ModifyD20>(c => {
                    c.RerollOnlyIfFailed = true;
                    c.Rule = RuleType.AttackRoll;
                    c.DispellOnRerollFinished = true;
                    c.RollsAmount = 1;
                    c.TakeBest = true;
                    c.Bonus = new ContextValue();
                    c.Chance = new ContextValue();
                    c.Value = new ContextValue();
                    c.Skill = new StatType[0];
                }));
            });
            var BloodragerDestinedDefyDeathResource = Helpers.Create<BlueprintAbilityResource>(bp => {
                bp.m_AssetGuid = Settings.Blueprints.NewBlueprints["BloodragerDestinedDefyDeathResource"];
                bp.name = "BloodragerDestinedDefyDeathResource";
                bp.m_Min = 0;
                bp.m_MaxAmount = new BlueprintAbilityResource.Amount {
                    BaseValue = 1,
                    IncreasedByStat = false,
                    m_Class = new BlueprintCharacterClassReference[0],
                    m_ClassDiv = new BlueprintCharacterClassReference[0],
                    m_Archetypes = new BlueprintArchetypeReference[0],
                    m_ArchetypesDiv = new BlueprintArchetypeReference[0]
                };
            });
            var BloodragerDestinedDefyDeath = Helpers.Create<BlueprintFeature>(bp => {
                bp.m_AssetGuid = Settings.Blueprints.NewBlueprints["BloodragerDestinedDefyDeath"];
                bp.name = "BloodragerDestinedDefyDeath";
                bp.SetName("Defy Death");
                bp.SetDescription("At 12th level, once per day when an attack or spell that deals damage would result in your death"
                    + ", you can attempt a DC 20 Fortitude save. If you succeed, you are instead reduced to 1 hit point; if you "
                    + "succeed and already have less than 1 hit point, you instead take no damage.");
                bp.AddComponent(Helpers.Create<AddAbilityResources>(c => {
                    c.m_Resource = BloodragerDestinedDefyDeathResource.ToReference<BlueprintAbilityResourceReference>();
                    c.RestoreAmount = true;
                }));
            });
            var BloodragerDestinedDefyDeathBuff = Helpers.Create<BlueprintBuff>(bp => {
                bp.m_AssetGuid = Settings.Blueprints.NewBlueprints["BloodragerDestinedDefyDeathBuff"];
                bp.name = "BloodragerDestinedDefyDeathBuff";
                bp.m_Flags = BlueprintBuff.Flags.HiddenInUi;
                bp.SetName(BloodragerDestinedDefyDeath.Name);
                bp.SetDescription(BloodragerDestinedDefyDeath.Description);
                bp.AddComponent(Helpers.Create<SurviveDeathWithSave>(c => {
                    c.DC = 20;
                    c.Type = SavingThrowType.Fortitude;
                    c.TargetHP = 1;
                    c.BlockIfBelowZero = true;
                    c.Resource = BloodragerDestinedDefyDeathResource.ToReference<BlueprintAbilityResourceReference>();
                    c.SpendAmount = 1;
                }));
            });
            var BloodragerDestinedUnstoppable = Helpers.Create<BlueprintFeature>(bp => {
                bp.m_AssetGuid = Settings.Blueprints.NewBlueprints["BloodragerDestinedUnstoppable"];
                bp.name = "BloodragerDestinedUnstoppable";
                bp.SetName("Unstoppable ");
                bp.SetDescription("At 16th level, any critical threats you score are automatically confirmed. Any critical "
                    + "threats made against you confirm only if the second roll results in a natural 20 (or is automatically confirmed).");
            });
            var BloodragerDestinedUnstoppableBuff = Helpers.Create<BlueprintBuff>(bp => {
                bp.m_AssetGuid = Settings.Blueprints.NewBlueprints["BloodragerDestinedUnstoppableBuff"];
                bp.name = "BloodragerDestinedUnstoppableBuff";
                bp.SetName(BloodragerDestinedUnstoppable.Name);
                bp.SetDescription(BloodragerDestinedUnstoppable.Description);
                bp.m_Flags = BlueprintBuff.Flags.HiddenInUi;
                bp.AddComponent(Helpers.Create<CriticalConfirmationACBonus>(c => {
                    c.Value = new ContextValue() {
                        ValueType = ContextValueType.Simple,
                    };
                    c.Bonus = 200;
                }));
                bp.AddComponent(Helpers.Create<InitiatorCritAutoconfirm>());
            });
            var BloodragerDestinedVictoryOrDeath = Helpers.Create<BlueprintFeature>(bp => {
                bp.m_AssetGuid = Settings.Blueprints.NewBlueprints["BloodragerDestinedVictoryOrDeath"];
                bp.name = "BloodragerDestinedVictoryOrDeath";
                bp.SetName("Victory or Death");
                bp.SetDescription("At 20th level, you are immune to paralysis and petrification, as well as to the stunned, dazed, "
                    + "and staggered conditions. You have these benefits constantly, even while not bloodraging.");
                bp.AddComponent(Helpers.Create<AddConditionImmunity>(c => {
                    c.Condition = UnitCondition.Paralyzed;
                }));
                bp.AddComponent(Helpers.Create<AddConditionImmunity>(c => {
                    c.Condition = UnitCondition.Petrified;
                }));
                bp.AddComponent(Helpers.Create<AddConditionImmunity>(c => {
                    c.Condition = UnitCondition.Stunned;
                }));
                bp.AddComponent(Helpers.Create<AddConditionImmunity>(c => {
                    c.Condition = UnitCondition.Dazed;
                }));
                bp.AddComponent(Helpers.Create<AddConditionImmunity>(c => {
                    c.Condition = UnitCondition.Staggered;
                }));
                bp.AddComponent(Helpers.Create<SpellImmunityToSpellDescriptor>(c => {
                    c.Descriptor = SpellDescriptor.Paralysis
                    | SpellDescriptor.Petrified
                    | SpellDescriptor.Stun
                    | SpellDescriptor.Daze
                    | SpellDescriptor.Staggered;
                }));
                bp.AddComponent(Helpers.Create<BuffDescriptorImmunity>(c => {
                    c.Descriptor = SpellDescriptor.Paralysis
                    | SpellDescriptor.Petrified
                    | SpellDescriptor.Stun
                    | SpellDescriptor.Daze
                    | SpellDescriptor.Staggered;
                }));
            });
            Resources.AddBlueprint(BloodragerDestinedStrikeResource);
            Resources.AddBlueprint(BloodragerDestinedStrikeResourceIncrease);
            Resources.AddBlueprint(BloodragerDestinedStrikeBuff);
            Resources.AddBlueprint(BloodragerDestinedStrikeAbility);
            Resources.AddBlueprint(BloodragerDestinedStrike);
            Resources.AddBlueprint(BloodragerDestinedFatedBloodrager);
            Resources.AddBlueprint(BloodragerDestinedFatedBloodragerBuff);
            Resources.AddBlueprint(BloodragerDestinedCertainStrike);
            Resources.AddBlueprint(BloodragerDestinedCertainStrikeBuff);
            Resources.AddBlueprint(BloodragerDestinedDefyDeathResource);
            Resources.AddBlueprint(BloodragerDestinedDefyDeath);
            Resources.AddBlueprint(BloodragerDestinedDefyDeathBuff);
            Resources.AddBlueprint(BloodragerDestinedUnstoppable);
            Resources.AddBlueprint(BloodragerDestinedUnstoppableBuff);
            Resources.AddBlueprint(BloodragerDestinedVictoryOrDeath);
            //Bloodline Feats
            var BloodragerDestinedFeatSelection = Helpers.Create<BlueprintFeatureSelection>(bp => {
                bp.m_AssetGuid = Settings.Blueprints.NewBlueprints["BloodragerDestinedFeatSelection"];
                bp.name = "BloodragerDestinedFeatSelection";
                bp.SetName("Bonus Feats");
                bp.SetDescription("Bonus Feats: Diehard, Endurance, Improved Initiative, Intimidating Prowess, Sieze The Moment, Lightning Reflexes, Weapon Focus.");
                bp.Ranks = 1;
                bp.IsClassFeature = true;
                bp.HideInUI = true;
                bp.HideNotAvailibleInUI = true;

                bp.m_Features = new BlueprintFeatureReference[] {
                    Diehard,
                    Endurance,
                    ImprovedInitiative,
                    IntimidatingProwess,
                    SiezeTheMoment,
                    LightningReflexes,
                    WeaponFocus
                };
                bp.m_AllFeatures = bp.m_Features;
            });
            var BloodragerDestinedFeatSelectionGreenrager = Helpers.Create<BlueprintFeatureSelection>(bp => {
                bp.m_AssetGuid = Settings.Blueprints.NewBlueprints["BloodragerDestinedFeatSelectionGreenrager"];
                bp.name = "BloodragerDestinedFeatSelectionGreenrager";
                bp.SetName(BloodragerDestinedFeatSelection.Name);
                bp.SetDescription(BloodragerDestinedFeatSelection.Description);
                bp.Ranks = 1;
                bp.IsClassFeature = true;
                bp.HideInUI = true;
                bp.HideNotAvailibleInUI = true;

                bp.m_Features = BloodragerDestinedFeatSelection.m_Features;
                bp.m_AllFeatures = bp.m_Features;
                bp.AddComponent(Helpers.Create<PrerequisiteNoArchetype>(c => {
                    c.HideInUI = true;
                    c.m_CharacterClass = BloodragerClass;
                    c.m_Archetype = GreenragerArchetype;
                }));
            });
            Resources.AddBlueprint(BloodragerDestinedFeatSelection);
            Resources.AddBlueprint(BloodragerDestinedFeatSelectionGreenrager);
            //Bloodline Spells
            var BloodragerDestinedSpell7 = Helpers.Create<BlueprintFeature>(bp => {
                var spell = MageShield;
                bp.m_AssetGuid = Settings.Blueprints.NewBlueprints["BloodragerDestinedSpell7"];
                bp.name = "BloodragerDestinedSpell7";
                bp.SetName($"Bonus Spell — {spell.Get().Name}");
                bp.SetDescription("At 7th, 10th, 13th, and 16th levels, a bloodrager learns an additional spell derived from his bloodline.");
                bp.AddComponent(Helpers.Create<AddKnownSpell>(c => {
                    c.m_CharacterClass = BloodragerClass;
                    c.m_Spell = spell;
                    c.SpellLevel = 1;
                }));
            });
            var BloodragerDestinedSpell10 = Helpers.Create<BlueprintFeature>(bp => {
                var spell = Blur;
                bp.m_AssetGuid = Settings.Blueprints.NewBlueprints["BloodragerDestinedSpell10"];
                bp.name = "BloodragerDestinedSpell10";
                bp.SetName($"Bonus Spell — {spell.Get().Name}");
                bp.SetDescription("At 7th, 10th, 13th, and 16th levels, a bloodrager learns an additional spell derived from his bloodline.");
                bp.AddComponent(Helpers.Create<AddKnownSpell>(c => {
                    c.m_CharacterClass = BloodragerClass;
                    c.m_Spell = spell;
                    c.SpellLevel = 2;
                }));
            });
            var BloodragerDestinedSpell13 = Helpers.Create<BlueprintFeature>(bp => {
                var spell = ProtectionFromEnergy;
                bp.m_AssetGuid = Settings.Blueprints.NewBlueprints["BloodragerDestinedSpell13"];
                bp.name = "BloodragerDestinedSpell13";
                bp.SetName($"Bonus Spell — {spell.Get().Name}");
                bp.SetDescription("At 7th, 10th, 13th, and 16th levels, a bloodrager learns an additional spell derived from his bloodline.");
                bp.AddComponent(Helpers.Create<AddKnownSpell>(c => {
                    c.m_CharacterClass = BloodragerClass;
                    c.m_Spell = spell;
                    c.SpellLevel = 3;
                }));
            });
            var BloodragerDestinedSpell16 = Helpers.Create<BlueprintFeature>(bp => {
                var spell = FreedomOfMovement;
                bp.m_AssetGuid = Settings.Blueprints.NewBlueprints["BloodragerDestinedSpell16"];
                bp.name = "BloodragerDestinedSpell16";
                bp.SetName($"Bonus Spell — {spell.Get().Name}");
                bp.SetDescription("At 7th, 10th, 13th, and 16th levels, a bloodrager learns an additional spell derived from his bloodline.");
                bp.AddComponent(Helpers.Create<AddKnownSpell>(c => {
                    c.m_CharacterClass = BloodragerClass;
                    c.m_Spell = spell;
                    c.SpellLevel = 4;
                }));
            });
            Resources.AddBlueprint(BloodragerDestinedSpell7);
            Resources.AddBlueprint(BloodragerDestinedSpell10);
            Resources.AddBlueprint(BloodragerDestinedSpell13);
            Resources.AddBlueprint(BloodragerDestinedSpell16);
            //Bloodline Core
            var BloodragerDestinedBloodline = Helpers.Create<BlueprintProgression>(bp => {
                bp.m_AssetGuid = Settings.Blueprints.NewBlueprints["BloodragerDestinedBloodline"];
                bp.name = "BloodragerDestinedBloodline";
                bp.SetName("Destined");
                bp.SetDescription("Your bloodline is destined for great things. When you bloodrage, you exude a greatness that makes all but the most legendary creatures seem lesser.\n"
                    + "Your future greatness grants you the might to strike your enemies with awe.\n"
                    + BloodragerDestinedFeatSelection.Description
                    + "\nBonus Spells: Shield (7th), Blur (10th), Protection From Energy (13th), Freedom Of Movement (16th).");
                bp.m_Classes = new BlueprintProgression.ClassWithLevel[] {
                    new BlueprintProgression.ClassWithLevel {
                        m_Class = BloodragerClass
                    }
                };
                bp.GiveFeaturesForPreviousLevels = true;
                bp.Groups = new FeatureGroup[] { FeatureGroup.BloodragerBloodline };
                bp.Ranks = 1;
                bp.IsClassFeature = true;
                bp.GiveFeaturesForPreviousLevels = true;
                bp.LevelEntries = new LevelEntry[] {
                    new LevelEntry(){ Level = 1, Features = { BloodragerDestinedStrike, DestinedBloodlineRequisiteFeature, BloodlineRequisiteFeature }},
                    new LevelEntry(){ Level = 4, Features = { BloodragerDestinedFatedBloodrager }},
                    new LevelEntry(){ Level = 6, Features = { BloodragerDestinedFeatSelectionGreenrager }},
                    new LevelEntry(){ Level = 7, Features = { BloodragerDestinedSpell7 }},
                    new LevelEntry(){ Level = 8, Features = { BloodragerDestinedCertainStrike, BloodragerDestinedFatedBloodrager }},
                    new LevelEntry(){ Level = 9, Features = { BloodragerDestinedFeatSelectionGreenrager }},
                    new LevelEntry(){ Level = 10, Features = { BloodragerDestinedSpell10 }},
                    new LevelEntry(){ Level = 12, Features = { BloodragerDestinedFeatSelection, BloodragerDestinedDefyDeath, BloodragerDestinedFatedBloodrager, BloodragerDestinedStrikeResourceIncrease }},
                    new LevelEntry(){ Level = 13, Features = { BloodragerDestinedSpell13 }},
                    new LevelEntry(){ Level = 15, Features = { BloodragerDestinedFeatSelection }},
                    new LevelEntry(){ Level = 16, Features = { BloodragerDestinedUnstoppable, BloodragerDestinedSpell16, BloodragerDestinedFatedBloodrager }},
                    new LevelEntry(){ Level = 18, Features = { BloodragerDestinedFeatSelection }},
                    new LevelEntry(){ Level = 20, Features = { BloodragerDestinedVictoryOrDeath, BloodragerDestinedFatedBloodrager }},
                };
                bp.AddComponent(Helpers.Create<PrerequisiteNoFeature>(c => {
                    c.Group = Prerequisite.GroupType.Any;
                    c.m_Feature = BloodlineRequisiteFeature;
                }));
                bp.AddComponent(Helpers.Create<PrerequisiteFeature>(c => {
                    c.Group = Prerequisite.GroupType.Any;
                    c.m_Feature = DestinedBloodlineRequisiteFeature;
                }));
            });
            var BloodragerDestinedBaseBuff = Helpers.Create<BlueprintBuff>(bp => {
                bp.name = "BloodragerDestinedBaseBuff";
                bp.SetName("Destined Bloodrage");
                bp.m_AssetGuid = Settings.Blueprints.NewBlueprints["BloodragerDestinedBaseBuff"];
                bp.m_Flags = BlueprintBuff.Flags.HiddenInUi;
            });

            BloodragerDestinedBaseBuff.AddConditionalBuff(BloodragerDestinedFatedBloodrager, BloodragerDestinedFatedBloodragerBuff);
            BloodragerDestinedBaseBuff.AddConditionalBuff(BloodragerDestinedCertainStrike, BloodragerDestinedCertainStrikeBuff);
            BloodragerDestinedBaseBuff.AddConditionalBuff(BloodragerDestinedDefyDeath, BloodragerDestinedDefyDeathBuff);
            BloodragerDestinedBaseBuff.AddConditionalBuff(BloodragerDestinedUnstoppable, BloodragerDestinedUnstoppableBuff);
            BloodragerDestinedBaseBuff.RemoveBuffAfterRage(BloodragerDestinedStrikeBuff);

            //Register Bloodrage Abilities
            BloodragerDestinedBaseBuff.ApplyBloodrageRestriction(BloodragerDestinedStrikeAbility);
            Resources.AddBlueprint(BloodragerDestinedBloodline);
            Resources.AddBlueprint(BloodragerDestinedBaseBuff);
            BloodragerStandardRageBuff.AddConditionalBuff(BloodragerDestinedBloodline, BloodragerDestinedBaseBuff);

            BloodlineTools.ApplyPrimalistException(BloodragerDestinedFatedBloodrager, 4, BloodragerDestinedBloodline);
            BloodlineTools.ApplyPrimalistException(BloodragerDestinedCertainStrike, 8, BloodragerDestinedBloodline);
            BloodlineTools.ApplyPrimalistException(BloodragerDestinedDefyDeath, 12, BloodragerDestinedBloodline);
            BloodlineTools.ApplyPrimalistException(BloodragerDestinedUnstoppable, 16, BloodragerDestinedBloodline);
            BloodlineTools.ApplyPrimalistException(BloodragerDestinedVictoryOrDeath, 20, BloodragerDestinedBloodline);
            if (!Settings.AddedContent.DestinedBloodline) { return; }
            BloodlineTools.RegisterBloodragerBloodline(BloodragerDestinedBloodline);
        }
        public static void AddSorcererDestinedBloodline() {
            var SorcererClass = ResourcesLibrary.TryGetBlueprint<BlueprintCharacterClass>("b3a505fb61437dc4097f43c3f8f9a4cf").ToReference<BlueprintCharacterClassReference>();
            var MagusClass = ResourcesLibrary.TryGetBlueprint<BlueprintCharacterClass>("45a4607686d96a1498891b3286121780").ToReference<BlueprintCharacterClassReference>();
            var EldritchScionArchetype = ResourcesLibrary.TryGetBlueprint<BlueprintArchetype>("d078b2ef073f2814c9e338a789d97b73").ToReference<BlueprintArchetypeReference>();
            //Used Assets
            var TrueSeeing = ResourcesLibrary.TryGetBlueprint<BlueprintAbility>("b3da3fbee6a751d4197e446c7e852bcb");
            var LawDomainBaseAbility = ResourcesLibrary.TryGetBlueprint<BlueprintAbility>("a970537ea2da20e42ae709c0bb8f793f");
            var ThoughtSense = ResourcesLibrary.TryGetBlueprint<BlueprintAbility>("8fb1a1670b6e1f84b89ea846f589b627");
            var BloodlineInfernalClassSkill = ResourcesLibrary.TryGetBlueprint<BlueprintFeature>("f07a37a5b245304429530842cb65e213");

            //Bonus Spells
            var MageShield = ResourcesLibrary.TryGetBlueprint<BlueprintAbility>("ef768022b0785eb43a18969903c537c4").ToReference<BlueprintAbilityReference>();
            var Blur = ResourcesLibrary.TryGetBlueprint<BlueprintAbility>("14ec7a4e52e90fa47a4c8d63c69fd5c1").ToReference<BlueprintAbilityReference>();
            var ProtectionFromEnergy = ResourcesLibrary.TryGetBlueprint<BlueprintAbility>("d2f116cfe05fcdd4a94e80143b67046f").ToReference<BlueprintAbilityReference>();
            var FreedomOfMovement = ResourcesLibrary.TryGetBlueprint<BlueprintAbility>("4c349361d720e844e846ad8c19959b1e").ToReference<BlueprintAbilityReference>();
            var BreakEnchantment = ResourcesLibrary.TryGetBlueprint<BlueprintAbility>("7792da00c85b9e042a0fdfc2b66ec9a8").ToReference<BlueprintAbilityReference>();
            var HeroismGreater = ResourcesLibrary.TryGetBlueprint<BlueprintAbility>("e15e5e7045fda2244b98c8f010adfe31").ToReference<BlueprintAbilityReference>();
            var CircleOfClarity = ResourcesLibrary.TryGetBlueprint<BlueprintAbility>("f333185ae986b2a45823cce86535a122").ToReference<BlueprintAbilityReference>();
            var ProtectionFromSpells = ResourcesLibrary.TryGetBlueprint<BlueprintAbility>("42aa71adc7343714fa92e471baa98d42").ToReference<BlueprintAbilityReference>();
            var Foresight = ResourcesLibrary.TryGetBlueprint<BlueprintAbility>("1f01a098d737ec6419aedc4e7ad61fdd").ToReference<BlueprintAbilityReference>();
            //Bonus Feats
            var ArcaneStrike = ResourcesLibrary.TryGetBlueprint<BlueprintFeature>("0ab2f21a922feee4dab116238e3150b4").ToReference<BlueprintFeatureReference>();
            var Diehard = ResourcesLibrary.TryGetBlueprint<BlueprintFeature>("86669ce8759f9d7478565db69b8c19ad").ToReference<BlueprintFeatureReference>();
            var Endurance = ResourcesLibrary.TryGetBlueprint<BlueprintFeature>("54ee847996c25cd4ba8773d7b8555174").ToReference<BlueprintFeatureReference>();
            var MaximizeSpell = ResourcesLibrary.TryGetBlueprint<BlueprintFeature>("7f2b282626862e345935bbea5e66424b").ToReference<BlueprintFeatureReference>();
            var SiezeTheMoment = ResourcesLibrary.TryGetBlueprint<BlueprintFeature>("1191ef3065e6f8e4f9fbe1b7e3c0f760").ToReference<BlueprintFeatureReference>();
            var LightningReflexes = ResourcesLibrary.TryGetBlueprint<BlueprintFeature>("15e7da6645a7f3d41bdad7c8c4b9de1e").ToReference<BlueprintFeatureReference>();
            var WeaponFocus = ResourcesLibrary.TryGetBlueprint<BlueprintFeature>("1e1f627d26ad36f43bbd26cc2bf8ac7e").ToReference<BlueprintFeatureReference>();
            var SkillFocusKnowledgeWorld = ResourcesLibrary.TryGetBlueprint<BlueprintFeature>("611e863120c0f9a4cab2d099f1eb20b4").ToReference<BlueprintFeatureReference>();
            //Bloodline Powers
            var SorcererDestinedClassSkill = Helpers.Create<BlueprintFeature>(bp => {
                bp.m_AssetGuid = Settings.Blueprints.NewBlueprints["SorcererDestinedClassSkill"];
                bp.name = "SorcererDestinedClassSkill";
                bp.SetName("Class Skill — Knowledge (World)");
                bp.SetDescription("Additional class skill from the destined bloodline.");
                bp.AddComponent(Helpers.Create<AddClassSkill>(c => {
                    c.Skill = StatType.SkillKnowledgeWorld;
                }));
                bp.IsClassFeature = true;
                bp.Ranks = 1;
                bp.m_Icon = BloodlineInfernalClassSkill.Icon;
            });
            var SorcererDestinedBloodlineArcanaBuff1 = Helpers.Create<BlueprintBuff>(bp => {
                bp.m_AssetGuid = Settings.Blueprints.NewBlueprints["SorcererDestinedBloodlineArcanaBuff1"];
                bp.name = "SorcererDestinedBloodlineArcanaBuff1";
                bp.m_Flags = BlueprintBuff.Flags.HiddenInUi;
                bp.SetName("Destined Bloodline Arcana");
                bp.AddComponent(Helpers.Create<AddStatBonus>(c => {
                    c.Descriptor = ModifierDescriptor.Luck;
                    c.Stat = StatType.SaveFortitude;
                    c.Value = 1;
                }));
                bp.AddComponent(Helpers.Create<AddStatBonus>(c => {
                    c.Descriptor = ModifierDescriptor.Luck;
                    c.Stat = StatType.SaveReflex;
                    c.Value = 1;
                }));
                bp.AddComponent(Helpers.Create<AddStatBonus>(c => {
                    c.Descriptor = ModifierDescriptor.Luck;
                    c.Stat = StatType.SaveWill;
                    c.Value = 1;
                }));
            });
            var SorcererDestinedBloodlineArcanaBuff2 = Helpers.Create<BlueprintBuff>(bp => {
                bp.m_AssetGuid = Settings.Blueprints.NewBlueprints["SorcererDestinedBloodlineArcanaBuff2"];
                bp.name = "SorcererDestinedBloodlineArcanaBuff2";
                bp.m_Flags = BlueprintBuff.Flags.HiddenInUi;
                bp.SetName("Destined Bloodline Arcana");
                bp.AddComponent(Helpers.Create<AddStatBonus>(c => {
                    c.Descriptor = ModifierDescriptor.Luck;
                    c.Stat = StatType.SaveFortitude;
                    c.Value = 2;
                }));
                bp.AddComponent(Helpers.Create<AddStatBonus>(c => {
                    c.Descriptor = ModifierDescriptor.Luck;
                    c.Stat = StatType.SaveReflex;
                    c.Value = 2;
                }));
                bp.AddComponent(Helpers.Create<AddStatBonus>(c => {
                    c.Descriptor = ModifierDescriptor.Luck;
                    c.Stat = StatType.SaveWill;
                    c.Value = 3;
                }));
            });
            var SorcererDestinedBloodlineArcanaBuff3 = Helpers.Create<BlueprintBuff>(bp => {
                bp.m_AssetGuid = Settings.Blueprints.NewBlueprints["SorcererDestinedBloodlineArcanaBuff3"];
                bp.name = "SorcererDestinedBloodlineArcanaBuff3";
                bp.m_Flags = BlueprintBuff.Flags.HiddenInUi;
                bp.SetName("Destined Bloodline Arcana");
                bp.AddComponent(Helpers.Create<AddStatBonus>(c => {
                    c.Descriptor = ModifierDescriptor.Luck;
                    c.Stat = StatType.SaveFortitude;
                    c.Value = 3;
                }));
                bp.AddComponent(Helpers.Create<AddStatBonus>(c => {
                    c.Descriptor = ModifierDescriptor.Luck;
                    c.Stat = StatType.SaveReflex;
                    c.Value = 3;
                }));
                bp.AddComponent(Helpers.Create<AddStatBonus>(c => {
                    c.Descriptor = ModifierDescriptor.Luck;
                    c.Stat = StatType.SaveWill;
                    c.Value = 3;
                }));
            });
            var SorcererDestinedBloodlineArcanaBuff4 = Helpers.Create<BlueprintBuff>(bp => {
                bp.m_AssetGuid = Settings.Blueprints.NewBlueprints["SorcererDestinedBloodlineArcanaBuff4"];
                bp.name = "SorcererDestinedBloodlineArcanaBuff4";
                bp.m_Flags = BlueprintBuff.Flags.HiddenInUi;
                bp.SetName("Destined Bloodline Arcana");
                bp.AddComponent(Helpers.Create<AddStatBonus>(c => {
                    c.Descriptor = ModifierDescriptor.Luck;
                    c.Stat = StatType.SaveFortitude;
                    c.Value = 4;
                }));
                bp.AddComponent(Helpers.Create<AddStatBonus>(c => {
                    c.Descriptor = ModifierDescriptor.Luck;
                    c.Stat = StatType.SaveReflex;
                    c.Value = 4;
                }));
                bp.AddComponent(Helpers.Create<AddStatBonus>(c => {
                    c.Descriptor = ModifierDescriptor.Luck;
                    c.Stat = StatType.SaveWill;
                    c.Value = 4;
                }));
            });
            var SorcererDestinedBloodlineArcanaBuff5 = Helpers.Create<BlueprintBuff>(bp => {
                bp.m_AssetGuid = Settings.Blueprints.NewBlueprints["SorcererDestinedBloodlineArcanaBuff5"];
                bp.name = "SorcererDestinedBloodlineArcanaBuff5";
                bp.m_Flags = BlueprintBuff.Flags.HiddenInUi;
                bp.SetName("Destined Bloodline Arcana");
                bp.AddComponent(Helpers.Create<AddStatBonus>(c => {
                    c.Descriptor = ModifierDescriptor.Luck;
                    c.Stat = StatType.SaveFortitude;
                    c.Value = 5;
                }));
                bp.AddComponent(Helpers.Create<AddStatBonus>(c => {
                    c.Descriptor = ModifierDescriptor.Luck;
                    c.Stat = StatType.SaveReflex;
                    c.Value = 5;
                }));
                bp.AddComponent(Helpers.Create<AddStatBonus>(c => {
                    c.Descriptor = ModifierDescriptor.Luck;
                    c.Stat = StatType.SaveWill;
                    c.Value = 5;
                }));
            });
            var SorcererDestinedBloodlineArcanaBuff6 = Helpers.Create<BlueprintBuff>(bp => {
                bp.m_AssetGuid = Settings.Blueprints.NewBlueprints["SorcererDestinedBloodlineArcanaBuff6"];
                bp.name = "SorcererDestinedBloodlineArcanaBuff6";
                bp.m_Flags = BlueprintBuff.Flags.HiddenInUi;
                bp.SetName("Destined Bloodline Arcana");
                bp.AddComponent(Helpers.Create<AddStatBonus>(c => {
                    c.Descriptor = ModifierDescriptor.Luck;
                    c.Stat = StatType.SaveFortitude;
                    c.Value = 6;
                }));
                bp.AddComponent(Helpers.Create<AddStatBonus>(c => {
                    c.Descriptor = ModifierDescriptor.Luck;
                    c.Stat = StatType.SaveReflex;
                    c.Value = 6;
                }));
                bp.AddComponent(Helpers.Create<AddStatBonus>(c => {
                    c.Descriptor = ModifierDescriptor.Luck;
                    c.Stat = StatType.SaveWill;
                    c.Value = 6;
                }));
            });
            var SorcererDestinedBloodlineArcanaBuff7 = Helpers.Create<BlueprintBuff>(bp => {
                bp.m_AssetGuid = Settings.Blueprints.NewBlueprints["SorcererDestinedBloodlineArcanaBuff7"];
                bp.name = "SorcererDestinedBloodlineArcanaBuff7";
                bp.m_Flags = BlueprintBuff.Flags.HiddenInUi;
                bp.SetName("Destined Bloodline Arcana");
                bp.AddComponent(Helpers.Create<AddStatBonus>(c => {
                    c.Descriptor = ModifierDescriptor.Luck;
                    c.Stat = StatType.SaveFortitude;
                    c.Value = 7;
                }));
                bp.AddComponent(Helpers.Create<AddStatBonus>(c => {
                    c.Descriptor = ModifierDescriptor.Luck;
                    c.Stat = StatType.SaveReflex;
                    c.Value = 7;
                }));
                bp.AddComponent(Helpers.Create<AddStatBonus>(c => {
                    c.Descriptor = ModifierDescriptor.Luck;
                    c.Stat = StatType.SaveWill;
                    c.Value = 7;
                }));
            });
            var SorcererDestinedBloodlineArcanaBuff8 = Helpers.Create<BlueprintBuff>(bp => {
                bp.m_AssetGuid = Settings.Blueprints.NewBlueprints["SorcererDestinedBloodlineArcanaBuff8"];
                bp.name = "SorcererDestinedBloodlineArcanaBuff8";
                bp.m_Flags = BlueprintBuff.Flags.HiddenInUi;
                bp.SetName("Destined Bloodline Arcana");
                bp.AddComponent(Helpers.Create<AddStatBonus>(c => {
                    c.Descriptor = ModifierDescriptor.Luck;
                    c.Stat = StatType.SaveFortitude;
                    c.Value = 8;
                }));
                bp.AddComponent(Helpers.Create<AddStatBonus>(c => {
                    c.Descriptor = ModifierDescriptor.Luck;
                    c.Stat = StatType.SaveReflex;
                    c.Value = 8;
                }));
                bp.AddComponent(Helpers.Create<AddStatBonus>(c => {
                    c.Descriptor = ModifierDescriptor.Luck;
                    c.Stat = StatType.SaveWill;
                    c.Value = 8;
                }));
            });
            var SorcererDestinedBloodlineArcanaBuff9 = Helpers.Create<BlueprintBuff>(bp => {
                bp.m_AssetGuid = Settings.Blueprints.NewBlueprints["SorcererDestinedBloodlineArcanaBuff9"];
                bp.name = "SorcererDestinedBloodlineArcanaBuff9";
                bp.m_Flags = BlueprintBuff.Flags.HiddenInUi;
                bp.SetName("Destined Bloodline Arcana");
                bp.AddComponent(Helpers.Create<AddStatBonus>(c => {
                    c.Descriptor = ModifierDescriptor.Luck;
                    c.Stat = StatType.SaveFortitude;
                    c.Value = 9;
                }));
                bp.AddComponent(Helpers.Create<AddStatBonus>(c => {
                    c.Descriptor = ModifierDescriptor.Luck;
                    c.Stat = StatType.SaveReflex;
                    c.Value = 9;
                }));
                bp.AddComponent(Helpers.Create<AddStatBonus>(c => {
                    c.Descriptor = ModifierDescriptor.Luck;
                    c.Stat = StatType.SaveWill;
                    c.Value = 9;
                }));
            });
            var SorcererDestinedBloodlineArcana = Helpers.Create<BlueprintFeature>(bp => {
                bp.m_AssetGuid = Settings.Blueprints.NewBlueprints["SorcererDestinedBloodlineArcana"];
                bp.name = "SorcererDestinedBloodlineArcana";
                bp.IsClassFeature = true;
                bp.SetName("Destined Bloodline Arcana");
                bp.SetDescription("Whenever you cast a spell with a range of “personal,” you gain a luck bonus equal to the spell’s level on all your saving throws for 1 round.");
                bp.AddComponent(Helpers.Create<DestinedArcanaComponent>(c => {
                    c.Buffs = new BlueprintBuffReference[] {
                        SorcererDestinedBloodlineArcanaBuff1.ToReference<BlueprintBuffReference>(),
                        SorcererDestinedBloodlineArcanaBuff2.ToReference<BlueprintBuffReference>(),
                        SorcererDestinedBloodlineArcanaBuff3.ToReference<BlueprintBuffReference>(),
                        SorcererDestinedBloodlineArcanaBuff4.ToReference<BlueprintBuffReference>(),
                        SorcererDestinedBloodlineArcanaBuff5.ToReference<BlueprintBuffReference>(),
                        SorcererDestinedBloodlineArcanaBuff6.ToReference<BlueprintBuffReference>(),
                        SorcererDestinedBloodlineArcanaBuff7.ToReference<BlueprintBuffReference>(),
                        SorcererDestinedBloodlineArcanaBuff8.ToReference<BlueprintBuffReference>(),
                        SorcererDestinedBloodlineArcanaBuff9.ToReference<BlueprintBuffReference>(),
                    };
                }));
            });
            var SorcererDestinedTouchOfDestinyResource = Helpers.Create<BlueprintAbilityResource>(bp => {
                bp.m_AssetGuid = Settings.Blueprints.NewBlueprints["SorcererDestinedTouchOfDestinyResource"];
                bp.name = "SorcererDestinedTouchOfDestinyResource";
                bp.m_Min = 0;
                bp.m_MaxAmount = new BlueprintAbilityResource.Amount {
                    BaseValue = 3,
                    IncreasedByStat = true,
                    ResourceBonusStat = StatType.Charisma,
                    m_Class = new BlueprintCharacterClassReference[0],
                    m_ClassDiv = new BlueprintCharacterClassReference[0],
                    m_Archetypes = new BlueprintArchetypeReference[0],
                    m_ArchetypesDiv = new BlueprintArchetypeReference[0]
                };
            });
            var SorcererDestinedTouchOfDestinyBuff = Helpers.Create<BlueprintBuff>(bp => {
                bp.m_AssetGuid = Settings.Blueprints.NewBlueprints["SorcererDestinedTouchOfDestinyBuff"];
                bp.name = "SorcererDestinedTouchOfDestinyBuff";
                bp.m_Icon = LawDomainBaseAbility.Icon;
                bp.SetName("Touch of Destiny");
                bp.SetDescription("");
                bp.AddComponent(Helpers.Create<AddContextStatBonus>(c => {
                    c.Descriptor = ModifierDescriptor.Insight;
                    c.Stat = StatType.AdditionalAttackBonus;
                    c.Value = new ContextValue {
                        ValueType = ContextValueType.Rank,
                        ValueRank = AbilityRankType.StatBonus
                    };
                }));
                bp.AddComponent(Helpers.Create<AddContextStatBonus>(c => {
                    c.Descriptor = ModifierDescriptor.Insight;
                    c.Stat = StatType.SaveFortitude;
                    c.Value = new ContextValue {
                        ValueType = ContextValueType.Rank,
                        ValueRank = AbilityRankType.StatBonus
                    };
                }));
                bp.AddComponent(Helpers.Create<AddContextStatBonus>(c => {
                    c.Descriptor = ModifierDescriptor.Insight;
                    c.Stat = StatType.SaveReflex;
                    c.Value = new ContextValue {
                        ValueType = ContextValueType.Rank,
                        ValueRank = AbilityRankType.StatBonus
                    };
                }));
                bp.AddComponent(Helpers.Create<AddContextStatBonus>(c => {
                    c.Descriptor = ModifierDescriptor.Insight;
                    c.Stat = StatType.SaveWill;
                    c.Value = new ContextValue {
                        ValueType = ContextValueType.Rank,
                        ValueRank = AbilityRankType.StatBonus
                    };
                }));
                bp.AddComponent(Helpers.Create<AddContextStatBonus>(c => {
                    c.Descriptor = ModifierDescriptor.Insight;
                    c.Stat = StatType.SkillAthletics;
                    c.Value = new ContextValue {
                        ValueType = ContextValueType.Rank,
                        ValueRank = AbilityRankType.StatBonus
                    };
                }));
                bp.AddComponent(Helpers.Create<AddContextStatBonus>(c => {
                    c.Descriptor = ModifierDescriptor.Insight;
                    c.Stat = StatType.SkillKnowledgeArcana;
                    c.Value = new ContextValue {
                        ValueType = ContextValueType.Rank,
                        ValueRank = AbilityRankType.StatBonus
                    };
                }));
                bp.AddComponent(Helpers.Create<AddContextStatBonus>(c => {
                    c.Descriptor = ModifierDescriptor.Insight;
                    c.Stat = StatType.SkillKnowledgeWorld;
                    c.Value = new ContextValue {
                        ValueType = ContextValueType.Rank,
                        ValueRank = AbilityRankType.StatBonus
                    };
                }));
                bp.AddComponent(Helpers.Create<AddContextStatBonus>(c => {
                    c.Descriptor = ModifierDescriptor.Insight;
                    c.Stat = StatType.SkillLoreNature;
                    c.Value = new ContextValue {
                        ValueType = ContextValueType.Rank,
                        ValueRank = AbilityRankType.StatBonus
                    };
                }));
                bp.AddComponent(Helpers.Create<AddContextStatBonus>(c => {
                    c.Descriptor = ModifierDescriptor.Insight;
                    c.Stat = StatType.SkillLoreReligion;
                    c.Value = new ContextValue {
                        ValueType = ContextValueType.Rank,
                        ValueRank = AbilityRankType.StatBonus
                    };
                }));
                bp.AddComponent(Helpers.Create<AddContextStatBonus>(c => {
                    c.Descriptor = ModifierDescriptor.Insight;
                    c.Stat = StatType.SkillMobility;
                    c.Value = new ContextValue {
                        ValueType = ContextValueType.Rank,
                        ValueRank = AbilityRankType.StatBonus
                    };
                }));
                bp.AddComponent(Helpers.Create<AddContextStatBonus>(c => {
                    c.Descriptor = ModifierDescriptor.Insight;
                    c.Stat = StatType.SkillPerception;
                    c.Value = new ContextValue {
                        ValueType = ContextValueType.Rank,
                        ValueRank = AbilityRankType.StatBonus
                    };
                }));
                bp.AddComponent(Helpers.Create<AddContextStatBonus>(c => {
                    c.Descriptor = ModifierDescriptor.Insight;
                    c.Stat = StatType.SkillPersuasion;
                    c.Value = new ContextValue {
                        ValueType = ContextValueType.Rank,
                        ValueRank = AbilityRankType.StatBonus
                    };
                }));
                bp.AddComponent(Helpers.Create<AddContextStatBonus>(c => {
                    c.Descriptor = ModifierDescriptor.Insight;
                    c.Stat = StatType.SkillStealth;
                    c.Value = new ContextValue {
                        ValueType = ContextValueType.Rank,
                        ValueRank = AbilityRankType.StatBonus
                    };
                }));
                bp.AddComponent(Helpers.Create<AddContextStatBonus>(c => {
                    c.Descriptor = ModifierDescriptor.Insight;
                    c.Stat = StatType.SkillThievery;
                    c.Value = new ContextValue {
                        ValueType = ContextValueType.Rank,
                        ValueRank = AbilityRankType.StatBonus
                    };
                }));
                bp.AddComponent(Helpers.Create<AddContextStatBonus>(c => {
                    c.Descriptor = ModifierDescriptor.Insight;
                    c.Stat = StatType.SkillUseMagicDevice;
                    c.Value = new ContextValue {
                        ValueType = ContextValueType.Rank,
                        ValueRank = AbilityRankType.StatBonus
                    };
                }));
                bp.AddComponent(Helpers.Create<ContextRankConfig>(c => {
                    c.m_Type = AbilityRankType.StatBonus;
                    c.m_BaseValueType = ContextRankBaseValueType.SummClassLevelWithArchetype;
                    c.m_Progression = ContextRankProgression.Div2;
                    c.m_StartLevel = 1;
                    c.m_StepLevel = 2;
                    c.m_Max = 20;
                    c.m_Min = 1;
                    c.m_UseMin = true;
                    c.m_Class = new BlueprintCharacterClassReference[] { SorcererClass, MagusClass };
                    c.Archetype = EldritchScionArchetype;
                }));
            });
            var SorcererDestinedTouchOfDestinyAbility = Helpers.Create<BlueprintAbility>(bp => {
                bp.m_AssetGuid = Settings.Blueprints.NewBlueprints["SorcererDestinedTouchOfDestinyAbility"];
                bp.name = "SorcererDestinedTouchOfDestinyAbility";
                bp.SetName("Touch of Destiny");
                bp.SetDescription("At 1st level, you can touch a creature as a standard action, giving it an insight bonus on attack rolls, skill checks, "
                    + "ability checks, and saving throws equal to 1/2 your sorcerer level (minimum 1) for 1 round. You can use this ability a number of "
                    + "times per day equal to 3 + your Charisma modifier.");
                bp.LocalizedDuration = new Kingmaker.Localization.LocalizedString();
                bp.LocalizedSavingThrow = new Kingmaker.Localization.LocalizedString();
                bp.CanTargetFriends = true;
                bp.Range = AbilityRange.Touch;
                bp.EffectOnAlly = AbilityEffectOnUnit.Helpful;
                bp.Animation = Kingmaker.Visual.Animation.Kingmaker.Actions.UnitAnimationActionCastSpell.CastAnimationStyle.Directional;
                bp.ActionType = Kingmaker.UnitLogic.Commands.Base.UnitCommand.CommandType.Standard;
                bp.m_Icon = LawDomainBaseAbility.Icon;
                bp.ResourceAssetIds = LawDomainBaseAbility.ResourceAssetIds;
                bp.AddComponent(Helpers.Create<AbilityResourceLogic>(c => {
                    c.m_RequiredResource = SorcererDestinedTouchOfDestinyResource.ToReference<BlueprintAbilityResourceReference>();
                    c.m_IsSpendResource = true;
                    c.Amount = 1;
                }));
                var addInsightBonus = Helpers.Create<ContextActionApplyBuff>(c => {
                    c.m_Buff = SorcererDestinedTouchOfDestinyBuff.ToReference<BlueprintBuffReference>();
                    c.IsNotDispelable = false;
                    c.Permanent = false;
                    c.DurationValue = new ContextDurationValue() {
                        Rate = DurationRate.Rounds,
                        DiceType = DiceType.One,
                        DiceCountValue = new ContextValue() {
                            ValueType = ContextValueType.Simple,
                            Value = 0
                        },
                        BonusValue = new ContextValue() {
                            ValueType = ContextValueType.Simple,
                            Value = 1
                        }
                    };
                });
                bp.AddComponent(Helpers.Create<AbilityEffectRunAction>(c => {
                    c.Actions = new ActionList {
                        Actions = new GameAction[] { addInsightBonus }
                    };
                }));
            });
            var SorcererDestinedTouchOfDestiny = Helpers.Create<BlueprintFeature>(bp => {
                bp.m_AssetGuid = Settings.Blueprints.NewBlueprints["SorcererDestinedTouchOfDestiny"];
                bp.name = "SorcererDestinedTouchOfDestiny";
                bp.SetName(SorcererDestinedTouchOfDestinyAbility.Name);
                bp.SetDescription(SorcererDestinedTouchOfDestinyAbility.Description);
                bp.AddComponent(Helpers.Create<AddFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[] {
                        SorcererDestinedTouchOfDestinyAbility.ToReference<BlueprintUnitFactReference>(),
                    };
                }));
                bp.AddComponent(Helpers.Create<AddAbilityResources>(c => {
                    c.m_Resource = SorcererDestinedTouchOfDestinyResource.ToReference<BlueprintAbilityResourceReference>();
                    c.RestoreAmount = true;
                }));
                bp.IsClassFeature = true;
                bp.Ranks = 1;
                bp.m_Icon = SorcererDestinedTouchOfDestinyAbility.Icon;
            });
            var SorcererDestinedWithinReachResource = Helpers.Create<BlueprintAbilityResource>(bp => {
                bp.m_AssetGuid = Settings.Blueprints.NewBlueprints["SorcererDestinedWithinReachResource"];
                bp.name = "SorcererDestinedWithinReachResource";
                bp.m_Min = 0;
                bp.m_MaxAmount = new BlueprintAbilityResource.Amount {
                    BaseValue = 1,
                    IncreasedByStat = false,
                    m_Class = new BlueprintCharacterClassReference[0],
                    m_ClassDiv = new BlueprintCharacterClassReference[0],
                    m_Archetypes = new BlueprintArchetypeReference[0],
                    m_ArchetypesDiv = new BlueprintArchetypeReference[0]
                };
            });
            var SorcererDestinedWithinReach = Helpers.Create<BlueprintFeature>(bp => {
                bp.m_AssetGuid = Settings.Blueprints.NewBlueprints["SorcererDestinedWithinReach"];
                bp.name = "SorcererDestinedWithinReach";
                bp.IsClassFeature = true;
                bp.SetName("Within Reach");
                bp.SetDescription("At 15th level, your ultimate destiny is drawing near. Once per day, when an attack or spell that causes "
                    + "damage would result in your death, you may attempt a DC 20 Will save. If successful, you are instead reduced to –1 hit "
                    + "points and are automatically stabilized. The bonus from your fated ability applies to this save.");
                bp.AddComponent(Helpers.Create<SurviveDeathWithSave>(c => {
                    c.DC = 20;
                    c.Type = SavingThrowType.Will;
                    c.TargetHP = -1;
                    c.BlockIfBelowZero = false;
                    c.Resource = SorcererDestinedWithinReachResource.ToReference<BlueprintAbilityResourceReference>();
                    c.SpendAmount = 1;
                }));
                bp.AddComponent(Helpers.Create<AddAbilityResources>(c => {
                    c.m_Resource = SorcererDestinedWithinReachResource.ToReference<BlueprintAbilityResourceReference>();
                    c.RestoreAmount = true;
                }));
            });
            var SorcererDestinedFatedBuff = Helpers.Create<BlueprintBuff>(bp => {
                bp.m_AssetGuid = Settings.Blueprints.NewBlueprints["SorcererDestinedFatedBuff"];
                bp.name = "SorcererDestinedFatedBuff";
                bp.m_Flags = BlueprintBuff.Flags.HiddenInUi;
                bp.SetName("Fated");
                bp.SetDescription("Starting at 3rd level, you gain a +1 luck bonus on all of your saving throws and to your AC during the first"
                    + "round of combat. At 7th level and every four levels thereafter, this bonus increases "
                    + "by +1, to a maximum of +5 at 19th level.");
                bp.AddComponent(Helpers.Create<AddContextStatBonus>(c => {
                    c.Descriptor = ModifierDescriptor.Luck;
                    c.Stat = StatType.SaveFortitude;
                    c.Value = new ContextValue {
                        ValueType = ContextValueType.Rank,
                        ValueRank = AbilityRankType.StatBonus
                    };
                }));
                bp.AddComponent(Helpers.Create<AddContextStatBonus>(c => {
                    c.Descriptor = ModifierDescriptor.Luck;
                    c.Stat = StatType.SaveReflex;
                    c.Value = new ContextValue {
                        ValueType = ContextValueType.Rank,
                        ValueRank = AbilityRankType.StatBonus
                    };
                }));
                bp.AddComponent(Helpers.Create<AddContextStatBonus>(c => {
                    c.Descriptor = ModifierDescriptor.Luck;
                    c.Stat = StatType.SaveWill;
                    c.Value = new ContextValue {
                        ValueType = ContextValueType.Rank,
                        ValueRank = AbilityRankType.StatBonus
                    };
                }));
                bp.AddComponent(Helpers.Create<AddContextStatBonus>(c => {
                    c.Descriptor = ModifierDescriptor.Luck;
                    c.Stat = StatType.AC;
                    c.Value = new ContextValue {
                        ValueType = ContextValueType.Rank,
                        ValueRank = AbilityRankType.StatBonus
                    };
                }));
                bp.AddComponent(Helpers.Create<ContextRankConfig>(c => {
                    c.m_Type = AbilityRankType.StatBonus;
                    c.m_BaseValueType = ContextRankBaseValueType.SummClassLevelWithArchetype;
                    c.m_Progression = ContextRankProgression.DelayedStartPlusDivStep;
                    c.m_StartLevel = 3;
                    c.m_StepLevel = 4;
                    c.m_Max = 20;
                    c.m_Min = 1;
                    c.m_UseMin = true;
                    c.m_Class = new BlueprintCharacterClassReference[] { SorcererClass, MagusClass };
                    c.Archetype = EldritchScionArchetype;
                }));
            });
            var SorcererDestinedFated = Helpers.Create<BlueprintFeature>(bp => {
                bp.m_AssetGuid = Settings.Blueprints.NewBlueprints["SorcererDestinedFated"];
                bp.name = "SorcererDestinedFated";
                bp.IsClassFeature = true;
                bp.Ranks = 5;
                bp.SetName("Fated");
                bp.SetDescription("Starting at 3rd level, you gain a +1 luck bonus on all of your saving throws and to your AC during the first"
                    + "round of combat or when you are otherwise unaware of an attack. At 7th level and every four levels thereafter, this bonus increases "
                    + "by +1, to a maximum of +5 at 19th level.");
                var fatedBuff = Helpers.Create<ContextActionApplyBuff>(c => {
                    c.m_Buff = SorcererDestinedFatedBuff.ToReference<BlueprintBuffReference>();
                    c.IsNotDispelable = true;
                    c.DurationValue = new ContextDurationValue() {
                        Rate = DurationRate.Rounds,
                        DiceType = DiceType.Zero,
                        DiceCountValue = new ContextValue() {
                            ValueType = ContextValueType.Simple,
                            Value = 0
                        },
                        BonusValue = new ContextValue() {
                            ValueType = ContextValueType.Simple,
                            Value = 1
                        }
                    };
                });
                bp.AddComponent(Helpers.Create<CombatStateTrigger>(c => {
                    c.CombatStartActions = new ActionList() {
                        Actions = new GameAction[] {
                            fatedBuff
                        }
                    };
                }));
                bp.AddComponent(Helpers.Create<SavingThrowBonusWhileUnaware>(c => {
                    c.Value = 1;
                    c.Descriptor = ModifierDescriptor.Luck;
                }));
                bp.AddComponent(Helpers.Create<SavingThrowBonusAgainstAbility>(c => {
                    c.m_CheckedFact = SorcererDestinedWithinReach.ToReference<BlueprintFeatureReference>();
                    c.Value = 1;
                    c.Descriptor = ModifierDescriptor.Luck;
                }));
            });
            var SorcererDestinedItWasMeantToBeResource = Helpers.Create<BlueprintAbilityResource>(bp => {
                bp.m_AssetGuid = Settings.Blueprints.NewBlueprints["SorcererDestinedItWasMeantToBeResource"];
                bp.name = "SorcererDestinedItWasMeantToBeResource";
                bp.m_Min = 0;
                bp.m_MaxAmount = new BlueprintAbilityResource.Amount {
                    BaseValue = 1,
                    IncreasedByStat = false,
                    m_Class = new BlueprintCharacterClassReference[0],
                    m_ClassDiv = new BlueprintCharacterClassReference[0],
                    m_Archetypes = new BlueprintArchetypeReference[0],
                    m_ArchetypesDiv = new BlueprintArchetypeReference[0]
                };
            });
            var SorcererDestinedItWasMeantToBeResourceIncrease = Helpers.Create<BlueprintFeature>(bp => {
                bp.m_AssetGuid = Settings.Blueprints.NewBlueprints["SorcererDestinedItWasMeantToBeResourceIncrease"];
                bp.name = "SorcererDestinedItWasMeantToBeResourceIncrease";
                bp.SetName("It Was Meant To Be (+1 Uses)");
                bp.SetDescription("It Was Meant To Be (+1 Uses)");
                bp.HideInUI = true;
                bp.AddComponent(Helpers.Create<IncreaseResourceAmount>(c => {
                    c.m_Resource = SorcererDestinedItWasMeantToBeResource.ToReference<BlueprintAbilityResourceReference>();
                    c.Value = 1;
                }));
            });
            var SorcererDestinedItWasMeantToBeBuff = Helpers.Create<BlueprintBuff>(bp => {
                bp.m_AssetGuid = Settings.Blueprints.NewBlueprints["SorcererDestinedItWasMeantToBeBuff"];
                bp.name = "SorcererDestinedItWasMeantToBeBuff";
                bp.SetName("It Was Meant To Be");
                bp.SetDescription("You may reroll any one attack roll, critical hit confirmation roll, or level check made to "
                    + "overcome spell resistance.");
                bp.AddComponent(Helpers.Create<ModifyD20>(c => {
                    c.RerollOnlyIfFailed = true;
                    c.RollsAmount = 1;
                    c.TakeBest = true;
                    c.Rule = RuleType.SpellResistance | RuleType.AttackRoll;
                    c.DispellOnRerollFinished = true;
                    c.Bonus = new ContextValue();
                    c.Chance = new ContextValue();
                    c.Value = new ContextValue();
                    c.Skill = new StatType[0];
                }));
            });
            var SorcererDestinedItWasMeantToBeAbility = Helpers.Create<BlueprintAbility>(bp => {
                bp.m_AssetGuid = Settings.Blueprints.NewBlueprints["SorcererDestinedItWasMeantToBeAbility"];
                bp.name = "SorcererDestinedItWasMeantToBeAbility";
                bp.SetName("It Was Meant To Be");
                bp.SetDescription("At 9th level, you may reroll any one attack roll, critical hit confirmation roll, or level check made to overcome spell resistance. "
                    + "At 17th level, you can use this ability twice per day.");
                bp.LocalizedDuration = new Kingmaker.Localization.LocalizedString();
                bp.LocalizedSavingThrow = new Kingmaker.Localization.LocalizedString();
                bp.CanTargetEnemies = true;
                bp.Range = AbilityRange.Personal;
                bp.EffectOnAlly = AbilityEffectOnUnit.Helpful;
                bp.Animation = Kingmaker.Visual.Animation.Kingmaker.Actions.UnitAnimationActionCastSpell.CastAnimationStyle.Directional;
                bp.ActionType = Kingmaker.UnitLogic.Commands.Base.UnitCommand.CommandType.Free;
                bp.m_Icon = TrueSeeing.Icon;
                bp.ResourceAssetIds = TrueSeeing.ResourceAssetIds;
                var addReroll = Helpers.Create<ContextActionApplyBuff>(c => {
                    c.m_Buff = SorcererDestinedItWasMeantToBeBuff.ToReference<BlueprintBuffReference>();
                    c.IsNotDispelable = true;
                    c.Permanent = true;
                    c.DurationValue = new ContextDurationValue() {
                        Rate = DurationRate.Rounds,
                        DiceType = DiceType.Zero,
                        DiceCountValue = new ContextValue() {
                            ValueType = ContextValueType.Simple,
                            Value = 0
                        },
                        BonusValue = new ContextValue() {
                            ValueType = ContextValueType.Simple,
                            Value = 1
                        }
                    };
                });
                bp.AddComponent(Helpers.Create<AbilityEffectRunAction>(c => {
                    c.Actions = new ActionList {
                        Actions = new GameAction[] { addReroll }
                    };
                }));
                bp.AddComponent(Helpers.Create<AbilityResourceLogic>(c => {
                    c.m_RequiredResource = SorcererDestinedItWasMeantToBeResource.ToReference<BlueprintAbilityResourceReference>();
                    c.m_IsSpendResource = true;
                    c.Amount = 1;
                }));
            });
            var SorcererDestinedItWasMeantToBe = Helpers.Create<BlueprintFeature>(bp => {
                bp.m_AssetGuid = Settings.Blueprints.NewBlueprints["SorcererDestinedItWasMeantToBe"];
                bp.name = "SorcererDestinedItWasMeantToBe";
                bp.IsClassFeature = true;
                bp.m_Icon = TrueSeeing.Icon;
                bp.SetName("It Was Meant To Be");
                bp.SetDescription(" At 9th level, you may reroll any one attack roll, critical hit confirmation roll, or level check made to overcome spell resistance. "
                    + "At 9th level, you can use this ability once per day. At 17th level, you can use this ability twice per day.");
                bp.AddComponent(Helpers.Create<AddFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[] {
                        SorcererDestinedItWasMeantToBeAbility.ToReference<BlueprintUnitFactReference>(),
                    };
                }));
                bp.AddComponent(Helpers.Create<AddAbilityResources>(c => {
                    c.m_Resource = SorcererDestinedItWasMeantToBeResource.ToReference<BlueprintAbilityResourceReference>();
                    c.RestoreAmount = true;
                }));
            });
            var SorcererDestinedDestinyRealizedResource = Helpers.Create<BlueprintAbilityResource>(bp => {
                bp.m_AssetGuid = Settings.Blueprints.NewBlueprints["SorcererDestinedDestinyRealizedResource"];
                bp.name = "SorcererDestinedDestinyRealizedResource";
                bp.m_Min = 0;
                bp.m_MaxAmount = new BlueprintAbilityResource.Amount {
                    BaseValue = 1,
                    IncreasedByStat = false,
                    m_Class = new BlueprintCharacterClassReference[0],
                    m_ClassDiv = new BlueprintCharacterClassReference[0],
                    m_Archetypes = new BlueprintArchetypeReference[0],
                    m_ArchetypesDiv = new BlueprintArchetypeReference[0]
                };
            });
            var SorcererDestinedDestinyRealizedBuff = Helpers.Create<BlueprintBuff>(bp => {
                bp.m_AssetGuid = Settings.Blueprints.NewBlueprints["SorcererDestinedDestinyRealizedBuff"];
                bp.name = "SorcererDestinedDestinyRealizedBuff";
                bp.SetName("Destiny Realized");
                bp.SetDescription("You automatically succeed at one caster level check made to overcome spell resistance.");
                bp.AddComponent(Helpers.Create<IgnoreSpellResistanceForSpells>(c => {
                    c.AllSpells = true;
                }));
                bp.AddComponent(Helpers.Create<RemoveBuffAfterSpellResistCheck>());
            });
            var SorcererDestinedDestinyRealizedAbility = Helpers.Create<BlueprintAbility>(bp => {
                bp.m_AssetGuid = Settings.Blueprints.NewBlueprints["SorcererDestinedDestinyRealizedAbility"];
                bp.name = "SorcererDestinedDestinyRealizedAbility";
                bp.SetName("Destiny Realized");
                bp.SetDescription("Once per day, you can automatically succeed at one caster level check made to overcome spell resistance. You must use this ability before making the roll.");
                bp.LocalizedDuration = new Kingmaker.Localization.LocalizedString();
                bp.LocalizedSavingThrow = new Kingmaker.Localization.LocalizedString();
                bp.Range = AbilityRange.Personal;
                bp.EffectOnAlly = AbilityEffectOnUnit.Harmful;
                bp.Animation = Kingmaker.Visual.Animation.Kingmaker.Actions.UnitAnimationActionCastSpell.CastAnimationStyle.Immediate;
                bp.ActionType = Kingmaker.UnitLogic.Commands.Base.UnitCommand.CommandType.Free;
                bp.m_Icon = ThoughtSense.Icon;
                bp.ResourceAssetIds = ThoughtSense.ResourceAssetIds;
                bp.AddComponent(Helpers.Create<AbilityResourceLogic>(c => {
                    c.m_RequiredResource = SorcererDestinedDestinyRealizedResource.ToReference<BlueprintAbilityResourceReference>();
                    c.m_IsSpendResource = true;
                    c.Amount = 1;
                }));
                var autoSpellPen = Helpers.Create<ContextActionApplyBuff>(c => {
                    c.m_Buff = SorcererDestinedDestinyRealizedBuff.ToReference<BlueprintBuffReference>();
                    c.IsNotDispelable = true;
                    c.Permanent = true;
                    c.DurationValue = new ContextDurationValue() {
                        Rate = DurationRate.Rounds,
                        DiceType = DiceType.Zero,
                        DiceCountValue = new ContextValue() {
                            ValueType = ContextValueType.Simple,
                            Value = 0
                        },
                        BonusValue = new ContextValue() {
                            ValueType = ContextValueType.Simple,
                            Value = 1
                        }
                    };
                });
                bp.AddComponent(Helpers.Create<AbilityEffectRunAction>(c => {
                    c.Actions = new ActionList();
                    c.Actions.Actions = new GameAction[] { autoSpellPen };
                }));
            });
            var SorcererDestinedDestinyRealized = Helpers.Create<BlueprintFeature>(bp => {
                bp.m_AssetGuid = Settings.Blueprints.NewBlueprints["SorcererDestinedDestinyRealized"];
                bp.name = "SorcererDestinedDestinyRealized";
                bp.m_Icon = SorcererDestinedDestinyRealizedAbility.Icon;
                bp.SetName("Destiny Realized");
                bp.SetDescription("At 20th level, your moment of destiny is at hand. Any critical threats made against you only confirm if the second "
                    + "roll results in a natural 20 on the die. Any critical threats you score with a spell are automatically confirmed. Once per day, you "
                    + "can automatically succeed at one caster level check made to overcome spell resistance. You must use this ability before making the roll.");
                bp.AddComponent(Helpers.Create<CriticalConfirmationACBonus>(c => {
                    c.Value = new ContextValue() {
                        ValueType = ContextValueType.Simple,
                    };
                    c.Bonus = 200;
                }));
                bp.AddComponent(Helpers.Create<AddFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[] {
                        SorcererDestinedDestinyRealizedAbility.ToReference<BlueprintUnitFactReference>(),
                    };
                }));
                bp.AddComponent(Helpers.Create<AddAbilityResources>(c => {
                    c.m_Resource = SorcererDestinedDestinyRealizedResource.ToReference<BlueprintAbilityResourceReference>();
                    c.RestoreAmount = true;
                }));
                bp.AddComponent(Helpers.Create<InitiatorSpellCritAutoconfirm>());
            });
            Resources.AddBlueprint(SorcererDestinedClassSkill);
            Resources.AddBlueprint(SorcererDestinedBloodlineArcanaBuff1);
            Resources.AddBlueprint(SorcererDestinedBloodlineArcanaBuff2);
            Resources.AddBlueprint(SorcererDestinedBloodlineArcanaBuff3);
            Resources.AddBlueprint(SorcererDestinedBloodlineArcanaBuff4);
            Resources.AddBlueprint(SorcererDestinedBloodlineArcanaBuff5);
            Resources.AddBlueprint(SorcererDestinedBloodlineArcanaBuff6);
            Resources.AddBlueprint(SorcererDestinedBloodlineArcanaBuff7);
            Resources.AddBlueprint(SorcererDestinedBloodlineArcanaBuff8);
            Resources.AddBlueprint(SorcererDestinedBloodlineArcanaBuff9);
            Resources.AddBlueprint(SorcererDestinedBloodlineArcana);
            Resources.AddBlueprint(SorcererDestinedTouchOfDestinyResource);
            Resources.AddBlueprint(SorcererDestinedTouchOfDestinyBuff);
            Resources.AddBlueprint(SorcererDestinedTouchOfDestinyAbility);
            Resources.AddBlueprint(SorcererDestinedTouchOfDestiny);
            Resources.AddBlueprint(SorcererDestinedFatedBuff);
            Resources.AddBlueprint(SorcererDestinedFated);
            Resources.AddBlueprint(SorcererDestinedItWasMeantToBeResource);
            Resources.AddBlueprint(SorcererDestinedItWasMeantToBeResourceIncrease);
            Resources.AddBlueprint(SorcererDestinedItWasMeantToBeBuff);
            Resources.AddBlueprint(SorcererDestinedItWasMeantToBeAbility);
            Resources.AddBlueprint(SorcererDestinedItWasMeantToBe);
            Resources.AddBlueprint(SorcererDestinedWithinReachResource);
            Resources.AddBlueprint(SorcererDestinedWithinReach);
            Resources.AddBlueprint(SorcererDestinedDestinyRealizedResource);
            Resources.AddBlueprint(SorcererDestinedDestinyRealizedBuff);
            Resources.AddBlueprint(SorcererDestinedDestinyRealizedAbility);
            Resources.AddBlueprint(SorcererDestinedDestinyRealized);
            //Bloodline Feats
            var SorcererDestinedFeatSelection = Helpers.Create<BlueprintFeatureSelection>(bp => {
                bp.m_AssetGuid = Settings.Blueprints.NewBlueprints["SorcererDestinedFeatSelection"];
                bp.name = "SorcererDestinedFeatSelection";
                bp.SetName("Bloodline Feat Selection");
                bp.SetDescription("At 7th level, and every six levels thereafter, a sorcerer receives one bonus feat, chosen from a list specific to each bloodline. "
                    + "The sorcerer must meet the prerequisites for these bonus feats."
                    + "\nBonus Feats: Arcane Strike, Diehard, Endurance, Sieze The Moment, Lightning Reflexes, Maximize Spell, Skill Focus (Knowledge World), Weapon Focus.");
                bp.Ranks = 1;
                bp.IsClassFeature = true;
                bp.HideInUI = true;
                bp.HideNotAvailibleInUI = true;

                bp.m_Features = new BlueprintFeatureReference[] {
                    ArcaneStrike,
                    Diehard,
                    Endurance,
                    SiezeTheMoment,
                    LightningReflexes,
                    WeaponFocus,
                    SkillFocusKnowledgeWorld,
                    MaximizeSpell
                };
                bp.m_AllFeatures = bp.m_Features;
            });
            Resources.AddBlueprint(SorcererDestinedFeatSelection);
            //Bloodline Spells
            var SorcererDestinedSpell3 = Helpers.Create<BlueprintFeature>(bp => {
                var Spell = MageShield;
                bp.m_AssetGuid = Settings.Blueprints.NewBlueprints["SorcererDestinedSpell3"];
                bp.name = "SorcererDestinedSpell3";
                bp.IsClassFeature = true;
                bp.SetName(Spell.Get().Name);
                bp.SetDescription("At 3rd level, and every two levels thereafter, a sorcerer learns an additional spell, derived from her bloodline.\n"
                    + $"{Spell.Get().Name}: {Spell.Get().Description}");
                bp.AddComponent(Helpers.Create<AddKnownSpell>(c => {
                    c.m_CharacterClass = SorcererClass;
                    c.m_Spell = Spell;
                    c.SpellLevel = 1;
                }));
                bp.m_Icon = Spell.Get().Icon;
            });
            var SorcererDestinedSpell5 = Helpers.Create<BlueprintFeature>(bp => {
                var Spell = Blur;
                bp.m_AssetGuid = Settings.Blueprints.NewBlueprints["SorcererDestinedSpell5"];
                bp.name = "SorcererDestinedSpell5";
                bp.IsClassFeature = true;
                bp.SetName(Spell.Get().Name);
                bp.SetDescription("At 3rd level, and every two levels thereafter, a sorcerer learns an additional spell, derived from her bloodline.\n"
                    + $"{Spell.Get().Name}: {Spell.Get().Description}");
                bp.AddComponent(Helpers.Create<AddKnownSpell>(c => {
                    c.m_CharacterClass = SorcererClass;
                    c.m_Spell = Spell;
                    c.SpellLevel = 2;
                }));
                bp.m_Icon = Spell.Get().Icon;
            });
            var SorcererDestinedSpell7 = Helpers.Create<BlueprintFeature>(bp => {
                var Spell = ProtectionFromEnergy;
                bp.m_AssetGuid = Settings.Blueprints.NewBlueprints["SorcererDestinedSpell7"];
                bp.name = "SorcererDestinedSpell7";
                bp.IsClassFeature = true;
                bp.SetName(Spell.Get().Name);
                bp.SetDescription("At 3rd level, and every two levels thereafter, a sorcerer learns an additional spell, derived from her bloodline.\n"
                    + $"{Spell.Get().Name}: {Spell.Get().Description}");
                bp.AddComponent(Helpers.Create<AddKnownSpell>(c => {
                    c.m_CharacterClass = SorcererClass;
                    c.m_Spell = Spell;
                    c.SpellLevel = 3;
                }));
                bp.m_Icon = Spell.Get().Icon;
            });
            var SorcererDestinedSpell9 = Helpers.Create<BlueprintFeature>(bp => {
                var Spell = FreedomOfMovement;
                bp.m_AssetGuid = Settings.Blueprints.NewBlueprints["SorcererDestinedSpell9"];
                bp.name = "SorcererDestinedSpell9";
                bp.IsClassFeature = true;
                bp.SetName(Spell.Get().Name);
                bp.SetDescription("At 3rd level, and every two levels thereafter, a sorcerer learns an additional spell, derived from her bloodline.\n"
                    + $"{Spell.Get().Name}: {Spell.Get().Description}");
                bp.AddComponent(Helpers.Create<AddKnownSpell>(c => {
                    c.m_CharacterClass = SorcererClass;
                    c.m_Spell = Spell;
                    c.SpellLevel = 4;
                }));
                bp.m_Icon = Spell.Get().Icon;
            });
            var SorcererDestinedSpell11 = Helpers.Create<BlueprintFeature>(bp => {
                var Spell = BreakEnchantment;
                bp.m_AssetGuid = Settings.Blueprints.NewBlueprints["SorcererDestinedSpell11"];
                bp.name = "SorcererDestinedSpell11";
                bp.IsClassFeature = true;
                bp.SetName(Spell.Get().Name);
                bp.SetDescription("At 3rd level, and every two levels thereafter, a sorcerer learns an additional spell, derived from her bloodline.\n"
                    + $"{Spell.Get().Name}: {Spell.Get().Description}");
                bp.AddComponent(Helpers.Create<AddKnownSpell>(c => {
                    c.m_CharacterClass = SorcererClass;
                    c.m_Spell = Spell;
                    c.SpellLevel = 5;
                }));
                bp.m_Icon = Spell.Get().Icon;
            });
            var SorcererDestinedSpell13 = Helpers.Create<BlueprintFeature>(bp => {
                var Spell = HeroismGreater;
                bp.m_AssetGuid = Settings.Blueprints.NewBlueprints["SorcererDestinedSpell13"];
                bp.name = "SorcererDestinedSpell13";
                bp.IsClassFeature = true;
                bp.SetName(Spell.Get().Name);
                bp.SetDescription("At 3rd level, and every two levels thereafter, a sorcerer learns an additional spell, derived from her bloodline.\n"
                    + $"{Spell.Get().Name}: {Spell.Get().Description}");
                bp.AddComponent(Helpers.Create<AddKnownSpell>(c => {
                    c.m_CharacterClass = SorcererClass;
                    c.m_Spell = Spell;
                    c.SpellLevel = 6;
                }));
                bp.m_Icon = Spell.Get().Icon;
            });
            var SorcererDestinedSpell15 = Helpers.Create<BlueprintFeature>(bp => {
                var Spell = CircleOfClarity;
                bp.m_AssetGuid = Settings.Blueprints.NewBlueprints["SorcererDestinedSpell15"];
                bp.name = "SorcererDestinedSpell15";
                bp.IsClassFeature = true;
                bp.SetName(Spell.Get().Name);
                bp.SetDescription("At 3rd level, and every two levels thereafter, a sorcerer learns an additional spell, derived from her bloodline.\n"
                    + $"{Spell.Get().Name}: {Spell.Get().Description}");
                bp.AddComponent(Helpers.Create<AddKnownSpell>(c => {
                    c.m_CharacterClass = SorcererClass;
                    c.m_Spell = Spell;
                    c.SpellLevel = 7;
                }));
                bp.m_Icon = Spell.Get().Icon;
            });
            var SorcererDestinedSpell17 = Helpers.Create<BlueprintFeature>(bp => {
                var Spell = ProtectionFromSpells;
                bp.m_AssetGuid = Settings.Blueprints.NewBlueprints["SorcererDestinedSpell17"];
                bp.name = "SorcererDestinedSpell17";
                bp.IsClassFeature = true;
                bp.SetName(Spell.Get().Name);
                bp.SetDescription("At 3rd level, and every two levels thereafter, a sorcerer learns an additional spell, derived from her bloodline.\n"
                    + $"{Spell.Get().Name}: {Spell.Get().Description}");
                bp.AddComponent(Helpers.Create<AddKnownSpell>(c => {
                    c.m_CharacterClass = SorcererClass;
                    c.m_Spell = Spell;
                    c.SpellLevel = 8;
                }));
                bp.m_Icon = Spell.Get().Icon;
            });
            var SorcererDestinedSpell19 = Helpers.Create<BlueprintFeature>(bp => {
                var Spell = Foresight;
                bp.m_AssetGuid = Settings.Blueprints.NewBlueprints["SorcererDestinedSpell19"];
                bp.name = "SorcererDestinedSpell19";
                bp.IsClassFeature = true;
                bp.SetName(Spell.Get().Name);
                bp.SetDescription("At 3rd level, and every two levels thereafter, a sorcerer learns an additional spell, derived from her bloodline.\n"
                    + $"{Spell.Get().Name}: {Spell.Get().Description}");
                bp.AddComponent(Helpers.Create<AddKnownSpell>(c => {
                    c.m_CharacterClass = SorcererClass;
                    c.m_Spell = Spell;
                    c.SpellLevel = 9;
                }));
                bp.m_Icon = Spell.Get().Icon;
            });
            Resources.AddBlueprint(SorcererDestinedSpell3);
            Resources.AddBlueprint(SorcererDestinedSpell5);
            Resources.AddBlueprint(SorcererDestinedSpell7);
            Resources.AddBlueprint(SorcererDestinedSpell9);
            Resources.AddBlueprint(SorcererDestinedSpell11);
            Resources.AddBlueprint(SorcererDestinedSpell13);
            Resources.AddBlueprint(SorcererDestinedSpell15);
            Resources.AddBlueprint(SorcererDestinedSpell17);
            Resources.AddBlueprint(SorcererDestinedSpell19);
            //Bloodline Core
            var SorcererDestinedBloodline = Helpers.Create<BlueprintProgression>(bp => {
                bp.m_AssetGuid = Settings.Blueprints.NewBlueprints["SorcererDestinedBloodline"];
                bp.name = "SorcererDestinedBloodline";
                bp.SetName("Destined Bloodline");
                bp.SetDescription("Your family is destined for greatness in some way. Your birth could have been foretold in prophecy, or perhaps "
                    + "it occurred during an especially auspicious event, such as a solar eclipse. Regardless of your bloodline’s origin, you have a great future ahead.\n"
                    + "Bonus Feats of the Aberrant Bloodline: Arcane Strike, Diehard, Endurance, Sieze The Moment, Lightning Reflexes, Maximize Spell, Skill Focus (Knowledge World), Weapon Focus.");
                bp.m_Classes = new BlueprintProgression.ClassWithLevel[] {
                    new BlueprintProgression.ClassWithLevel {
                        m_Class = SorcererClass
                    },
                    new BlueprintProgression.ClassWithLevel {
                        m_Class = MagusClass
                    }
                };
                bp.m_Archetypes = new BlueprintProgression.ArchetypeWithLevel[]{
                    new BlueprintProgression.ArchetypeWithLevel {
                        m_Archetype = EldritchScionArchetype
                    }
                };
                bp.Groups = new FeatureGroup[] { FeatureGroup.BloodragerBloodline };
                bp.Ranks = 1;
                bp.IsClassFeature = true;
                bp.GiveFeaturesForPreviousLevels = true;
                bp.LevelEntries = new LevelEntry[] {
                    new LevelEntry(){ Level = 1, Features = { SorcererDestinedTouchOfDestiny, SorcererDestinedBloodlineArcana, SorcererDestinedClassSkill, DestinedBloodlineRequisiteFeature, BloodlineRequisiteFeature }},
                    new LevelEntry(){ Level = 3, Features = { SorcererDestinedSpell3, SorcererDestinedFated }},
                    new LevelEntry(){ Level = 5, Features = { SorcererDestinedSpell5 }},
                    new LevelEntry(){ Level = 7, Features = { SorcererDestinedSpell7, SorcererDestinedFated }},
                    new LevelEntry(){ Level = 9, Features = { SorcererDestinedSpell9, SorcererDestinedItWasMeantToBe }},
                    new LevelEntry(){ Level = 11, Features = { SorcererDestinedSpell11, SorcererDestinedFated }},
                    new LevelEntry(){ Level = 13, Features = { SorcererDestinedSpell13 }},
                    new LevelEntry(){ Level = 15, Features = { SorcererDestinedSpell15, SorcererDestinedWithinReach, SorcererDestinedFated }},
                    new LevelEntry(){ Level = 17, Features = { SorcererDestinedSpell17 }},
                    new LevelEntry(){ Level = 19, Features = { SorcererDestinedSpell19, SorcererDestinedFated }},
                    new LevelEntry(){ Level = 20, Features = { SorcererDestinedDestinyRealized }},
                };
                bp.AddComponent(Helpers.Create<PrerequisiteNoFeature>(c => {
                    c.Group = Prerequisite.GroupType.Any;
                    c.m_Feature = BloodlineRequisiteFeature;
                }));
                bp.AddComponent(Helpers.Create<PrerequisiteFeature>(c => {
                    c.Group = Prerequisite.GroupType.Any;
                    c.m_Feature = DestinedBloodlineRequisiteFeature;
                }));
            });
            var CrossbloodedDestinedBloodline = Helpers.Create<BlueprintProgression>(bp => {
                bp.m_AssetGuid = Settings.Blueprints.NewBlueprints["CrossbloodedDestinedBloodline"];
                bp.name = "CrossbloodedDestinedBloodline";
                bp.SetName(SorcererDestinedBloodline.Name);
                bp.SetDescription(SorcererDestinedBloodline.Description);
                bp.m_Classes = new BlueprintProgression.ClassWithLevel[] {
                    new BlueprintProgression.ClassWithLevel {
                        m_Class = SorcererClass
                    },
                    new BlueprintProgression.ClassWithLevel {
                        m_Class = MagusClass
                    }
                };
                bp.m_Archetypes = new BlueprintProgression.ArchetypeWithLevel[]{
                    new BlueprintProgression.ArchetypeWithLevel {
                        m_Archetype = EldritchScionArchetype
                    }
                };
                bp.Groups = new FeatureGroup[] { FeatureGroup.BloodragerBloodline };
                bp.Ranks = 1;
                bp.IsClassFeature = true;
                bp.GiveFeaturesForPreviousLevels = true;
                bp.LevelEntries = new LevelEntry[] {
                    new LevelEntry(){ Level = 1, Features = { SorcererDestinedBloodlineArcana, SorcererDestinedClassSkill, DestinedBloodlineRequisiteFeature, BloodlineRequisiteFeature }},
                    new LevelEntry(){ Level = 3, Features = { SorcererDestinedSpell3 }},
                    new LevelEntry(){ Level = 5, Features = { SorcererDestinedSpell5 }},
                    new LevelEntry(){ Level = 7, Features = { SorcererDestinedSpell7 }},
                    new LevelEntry(){ Level = 9, Features = { SorcererDestinedSpell9 }},
                    new LevelEntry(){ Level = 11, Features = { SorcererDestinedSpell11 }},
                    new LevelEntry(){ Level = 13, Features = { SorcererDestinedSpell13 }},
                    new LevelEntry(){ Level = 15, Features = { SorcererDestinedSpell15 }},
                    new LevelEntry(){ Level = 17, Features = { SorcererDestinedSpell17 }},
                    new LevelEntry(){ Level = 19, Features = { SorcererDestinedSpell19 }}
                };
            });
            var SeekerDestinedBloodline = Helpers.Create<BlueprintProgression>(bp => {
                bp.m_AssetGuid = Settings.Blueprints.NewBlueprints["SeekerDestinedBloodline"];
                bp.name = "SeekerDestinedBloodline";
                bp.SetName(SorcererDestinedBloodline.Name);
                bp.SetDescription(SorcererDestinedBloodline.Description);
                bp.m_Classes = new BlueprintProgression.ClassWithLevel[] {
                    new BlueprintProgression.ClassWithLevel {
                        m_Class = SorcererClass
                    },
                    new BlueprintProgression.ClassWithLevel {
                        m_Class = MagusClass
                    }
                };
                bp.m_Archetypes = new BlueprintProgression.ArchetypeWithLevel[]{
                    new BlueprintProgression.ArchetypeWithLevel {
                        m_Archetype = EldritchScionArchetype
                    }
                };
                bp.GiveFeaturesForPreviousLevels = true;
                bp.Groups = new FeatureGroup[] { FeatureGroup.BloodragerBloodline };
                bp.Ranks = 1;
                bp.IsClassFeature = true;
                bp.GiveFeaturesForPreviousLevels = true;
                bp.LevelEntries = new LevelEntry[] {
                    new LevelEntry(){ Level = 1, Features = { SorcererDestinedTouchOfDestiny, SorcererDestinedBloodlineArcana, SorcererDestinedClassSkill, DestinedBloodlineRequisiteFeature, BloodlineRequisiteFeature }},
                    new LevelEntry(){ Level = 3, Features = { SorcererDestinedSpell3 }},
                    new LevelEntry(){ Level = 5, Features = { SorcererDestinedSpell5 }},
                    new LevelEntry(){ Level = 7, Features = { SorcererDestinedSpell7 }},
                    new LevelEntry(){ Level = 9, Features = { SorcererDestinedSpell9, SorcererDestinedItWasMeantToBe }},
                    new LevelEntry(){ Level = 11, Features = { SorcererDestinedSpell11 }},
                    new LevelEntry(){ Level = 13, Features = { SorcererDestinedSpell13 }},
                    new LevelEntry(){ Level = 15, Features = { SorcererDestinedSpell15 }},
                    new LevelEntry(){ Level = 17, Features = { SorcererDestinedSpell17 }},
                    new LevelEntry(){ Level = 19, Features = { SorcererDestinedSpell19 }},
                    new LevelEntry(){ Level = 20, Features = { SorcererDestinedDestinyRealized }},
                };
                bp.AddComponent(Helpers.Create<PrerequisiteNoFeature>(c => {
                    c.Group = Prerequisite.GroupType.Any;
                    c.m_Feature = BloodlineRequisiteFeature;
                }));
                bp.AddComponent(Helpers.Create<PrerequisiteFeature>(c => {
                    c.Group = Prerequisite.GroupType.Any;
                    c.m_Feature = DestinedBloodlineRequisiteFeature;
                }));
            });
            BloodlineTools.RegisterSorcererFeatSelection(SorcererDestinedFeatSelection, SorcererDestinedBloodline);

            Resources.AddBlueprint(SorcererDestinedBloodline);
            Resources.AddBlueprint(CrossbloodedDestinedBloodline);
            Resources.AddBlueprint(SeekerDestinedBloodline);

            if (!Settings.AddedContent.DestinedBloodline) { return; }
            BloodlineTools.RegisterSorcererBloodline(SorcererDestinedBloodline);
            BloodlineTools.RegisterCrossbloodedBloodline(CrossbloodedDestinedBloodline);
            BloodlineTools.RegisterSeekerBloodline(SeekerDestinedBloodline);
        }
    }
}