namespace RecipesRemixed.Recipes.Data
{
    public class DataConstants
    {
        public class Recipes
        {
            public const int MinDescriptionLength = 30;
            public const int MaxDescriptionLength = 5000;
        }

        public class Chef
        {
            public const int MinNameLength = 2;
            public const int MaxNameLength = 50;
        }

        public class Url
        {
            public const int MinUrlLength = 5;
            public const int MaxUrlLength = 2083;
        }

        public class Calories
        {
            public const int MinCalLength = 1;
            public const int MaxCalLength = 4;
            public const string CaloriesRegEx = @"\+[0-9]*";
        }
    }
}
