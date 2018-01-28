using UnityEngine;
using System.Collections;

public class SoldierType {

	public int SoldierTypeID;//兵种ID
	public string SoldierTypeName;//兵种名称
	public int AttackType;//兵种攻击类型,0为无法攻击，1为近战普通攻击不用放子弹，2为远程普通攻击需要放子弹,3为远程信仰攻击(射程内范围打击)
	public int CommandType;
	public float NormalMoveSpeed;//兵种正常状态移动速度
	public int NormalHP;//兵种普通HP
	public float AttackRange;//兵种射程
	public float AttackTime;//兵种攻击时间间隔
	public int NormalAttackPower;//兵种普通攻击伤害
	public int ArmorAgainstNormalAttack;//兵种抵抗普通攻击护甲值
	public bool MakePikeEffect;//兵种会不会造成长矛减速效果
	public bool BePikeAffected;//兵种会不会造成长矛减速效果
	public bool JoinCombat;//兵种是否参与战斗，0为不参与战斗（不会被选作攻击目标也不会攻击敌人），1为参与战斗
	public float LifeTime;//兵种默认存活时间，0为永久存活不限时，大于0则为自动存活时长

}
