public static class GameEvent
{
    public const string ROOM_INTERFACE_INIT = "ROOM_INTERFACE_INIT";
    public const string ABILITY_INFO = "ABILITY_INFO";
    public const string ATTACK_RESULT_INFO = "ATTACK_RESULT_INFO";
    public const string ENEMY_HIT = "ENEMY_HIT";
    public const string HERO_TURN = "HERO_TURN";
    public const string END_OF_TURN = "END_OF_TURN";
    public const string ATTACK_MOMENT = "ATTACK_MOMENT";
    public const string UPDATE_ENERGY_BARS = "UPDATE_ENERGY_BARS";
    public const string ON_ENEMY_TURN = "ON_ENEMY_TURN";
    public const string ABILITY_BUTTON_CLICK = "ABILITY_BUTTON_CLICK";

    //AI events
    public const string AI_INIT = "AI_INIT";
    public const string AI_STRATEGY_SELECT = "AI_STRATEGY_SELECT";

    //Input events
    public const string ON_LEFT_MOUSE_BUTTON_DOWN = "LEFT_MOUSE_BUTTON_DOWN";
}