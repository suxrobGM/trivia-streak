public static class GameConstant
{
	public static int FunHighScore { get; set; }
	public static int PrizeHighScore { get; set; }

	public static int CurrentScore { get; set; }
	public static int CorrectAnswer { get { return 25; } }
    public static int CorrectEasyAnswer { get { return 0; } }
    public static int CorrectMediumAnswer { get { return 5; } }
    public static int CorrectHardAnswer { get { return 10; } }

    public static int CorrectAnswerInSevenToTenSec { get { return 5; } }
    public static int CorrectAnswerInSixToFourSec { get { return 2; } }
    public static int CorrectAnswerInThreeToZeroSec { get { return 0; } }

    public static int CorrectAnswerCount { get; set; }
    public static int DifficultyAnswerCount { get; set; }
    public static int SpeedAnswerCount { get; set; }

    public static string UserId { get; set; }
    public static string UserType { get; set; }
    public static string UserEmail { get; set; }
    public static string UserCountry { get; set; }
    public static int IsMusic { get; set; }
    public static int IsSound { get; set; }
}
