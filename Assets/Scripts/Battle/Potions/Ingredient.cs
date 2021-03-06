
namespace CodeBrewery.Glime.Battle.Potions
{
    /// <summary>
    /// Provides a set of recipes.
    /// </summary>
    public class Ingredient
    {
        /// <summary>
        /// Gets the type of the ingredient.
        /// </summary>
        public IngredientType Type { get; private set; }

        /// <summary>
        /// The types 
        /// </summary>
        public ReadonlyPotionTypeSet PotionTypes { get; private set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="Ingredient"/> class.
        /// </summary>
        /// <param name="type">The type of the ingredient.</param>
        /// <param name="potionType">The types of the potions required for bu</param>
        public Ingredient(IngredientType type, ReadonlyPotionTypeSet potionType)
        {
            this.Type = type;
            this.PotionTypes = potionType;
        }

        /// <summary>
        /// Creates a new <see cref="IngredientType.FrailLavaBloom"/> ingredient.
        /// </summary>
        /// <returns>A new <see cref="IngredientType.FrailLavaBloom"/> ingredient.</returns>
        public static Ingredient FrailLavabloom { get; } = new Ingredient(
            IngredientType.FrailLavaBloom,
            new PotionTypeSet() {
                { PotionType.Fire, 2 },
                { PotionType.Weakness, 1}
            }.ToReadonly());

        /// <summary>
        /// Creates a new <see cref="IngredientType.FrostMint"/> ingredient.
        /// </summary>
        /// <returns>A new <see cref="IngredientType.FrostMint"/> ingredient.</returns>
        public static Ingredient FrostMint { get; } = new Ingredient(
            IngredientType.FrostMint,
            new PotionTypeSet() {
                { PotionType.Ice, 2 },
                { PotionType.Fire, 1}
            }.ToReadonly());

        /// <summary>
        /// Creates a new <see cref="IngredientType.LukewarmBeserkium"/> ingredient.
        /// </summary>
        /// <returns>A new <see cref="IngredientType.LukewarmBeserkium"/> ingredient.</returns>
        public static Ingredient LukewarmBerserkium { get; } = new Ingredient(
            IngredientType.LukewarmBeserkium,
            new PotionTypeSet() {
                { PotionType.Strength, 1 },
                { PotionType.Ice, 1},
                { PotionType.Fire, 1 }
            }.ToReadonly());

        /// <summary>
        /// Creates a new <see cref="IngredientType.JuviBerries"/> ingredient.
        /// </summary>
        /// <returns>A new <see cref="IngredientType.JuviBerries"/> ingredient.</returns>
        public static Ingredient JuviBerries { get; } = new Ingredient(
            IngredientType.JuviBerries,
            new PotionTypeSet() {
                { PotionType.Healing, 2 }
            }.ToReadonly());

        /// <summary>
        /// Creates a new <see cref="IngredientType.Paraleaf"/> ingredient.
        /// </summary>
        /// <returns>A new <see cref="IngredientType.Paraleaf"/> ingredient.</returns>
        public static Ingredient Paraleaf { get; } = new Ingredient(
            IngredientType.Paraleaf,
            new PotionTypeSet() {
                { PotionType.Electric, 1 }
            }.ToReadonly());

        /// <summary>
        /// Gets an instance of an ingredient.
        /// </summary>
        /// <param name="type">The type of the ingredient to get.</param>
        /// <returns>An instance of an ingredient with the specified <paramref name="type"/>.</returns>
        /// <exception cref="System.Exception">An ingredient with the specified <paramref name="type"/> does not exist.</exception>
        public static Ingredient GetIngredient(IngredientType type) => type switch
        {
            IngredientType.FrailLavaBloom => FrailLavabloom,
            IngredientType.FrostMint => FrostMint,
            IngredientType.LukewarmBeserkium => LukewarmBerserkium,
            IngredientType.JuviBerries => JuviBerries,
            IngredientType.Paraleaf => Paraleaf,
            _ => throw new System.Exception("Unkown type \"" + type.ToString() + "\"")
        };
    }
}