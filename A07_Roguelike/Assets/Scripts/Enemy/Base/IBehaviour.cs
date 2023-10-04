using System;

public enum StrategyState
{
    Rest, // ��� ���� ( �ൿ�� ���� ������ üũ) 
    Ready,// ������ ������ ���� ���� ������Ʈ�� ���¸� Ȯ���ϰ� ������ �� �ִ��� Ȯ��.
    Action,// ��⿭�� �� ����
    CoolTime, // ��Ÿ��

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