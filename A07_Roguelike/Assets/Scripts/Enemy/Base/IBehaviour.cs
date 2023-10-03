using System;

public enum StrategyState
{
    Rest, // 대기 상태 ( 행동에 대한 조건을 체크) 
    Ready,// 조건을 만족한 상태 현재 오브젝트의 상태를 확인하고 실행할 수 있는지 확인.
    Action,// 대기열에 들어간 상태
    CoolTime, // 쿨타임

}
public enum StratgeyType
{
    Move,
    Hurt,
    Attack,
    Skill,
    Dead,
    EndPoint,
}
public interface IBehaviour
{
    StrategyState State { get; set; }
    StratgeyType Type { get;}
    void OnRest();
    void OnAction();
    void OffAction();
    void OnCoolTime();
}