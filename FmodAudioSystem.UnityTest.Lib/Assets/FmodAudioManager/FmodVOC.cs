using FMOD;

public class FmodVOC : Singleton<FmodVOC>
{
    //----待机----
    public Sound 请投币;
    //public Sound 敲击乐翻天大家一起来暴击小怪兽;
    public Sound[] 待机语音;
    public Sound[] 小游戏标题;

    //----选择中----
    public Sound 币数不足请投币;
    //public Sound 敲击选择游戏;
    public Sound 单人游戏开始;
    public Sound 等待其他玩家选择;
    public Sound 等待超时;
    public Sound 联机对战乐趣无限;
    public Sound 请稍候;
    public Sound 请选择游戏模式;
    public Sound[] 玩法说明;

    //----游戏中----
    //01提示
    public Sound 开始;
    public Sound 预备;
    //02鼓励
    public Sound 不错;
    public Sound 加油加油;
    public Sound 好样的;
    //03提示时间
    public Sound 时间到;
    public Sound 时间快到了;
    //04提示_红包游戏
    public Sound 哇哦土豪发红包了争取得到最高分;
    public Sound 进入红包游戏;
    //05续币提示
    public Sound 游戏结束;
    //public Sound 请敲击选择继续游戏;
    //06奖励道具
    public Sound 人品爆发了;
    public Sound 恭喜获得代币;
    public Sound 恭喜获得宝石;
    public Sound 恭喜获得彩票;
    //07宝石触发的效果
    public Sound 冰雪飞尘;
    public Sound 双倍分数;
    public Sound 奖励1000分;
    public Sound 时间充值;
    public Sound 火焰流星;
    public Sound 能量锤子;
    //08得到双倍得分或大锤子时
    public Sound 双倍得分快抓紧时间消灭小怪兽;
    //public Sound 鸿运当头使劲敲;
    //09扣时间时提示
    public Sound 小心时间减少了;


    //----游戏玩法提示----
    //敲击守护员
    public Sound 注意别让怪物越过防御线;
    public Sound 加油努力消灭分裂出来的小怪;
    //暴击机械怪
    //public Sound 大怪冒头了赶紧敲击;
    public Sound 注意怪物冒头时才能打到它哦;
    //轰击敲敲萌
    public Sound 砰砰砰出来的怪物一个都别放过;
    //public Sound 敲击炮台轰炸怪物;


    //----结算----
    //提示
    public Sound 还要再来玩哦;
    //提示_联机比赛
    public Sound 领先了真厉害;
    public Sound 落后了加油加油;
    public Sound 平局双方不分上下呢;
    public Sound 恭喜获胜;
    public Sound 真可惜继续加油哦;
    //提示_转盘
    //public Sound 请敲击停止转盘;
    //结束
    public Sound 欢迎下次挑战;

    //----jp大奖----
    //提示
    public Sound 真走运集齐宝石进入终极大奖;
    //public Sound 敲中星星会有额外奖励哦;
    //public Sound 请敲击屏幕开始抽奖;
    //敲中星星时奖励票数
    public Sound 哇哦额外奖励100票;
    public Sound 人品爆发啦额外奖励1000票;

    public void InitSounds()
    {
        FmodSoundCreater fsc = FmodSoundCreater.instance;

        //----待机----
        fsc.InitSound("VOC/待机/请投币", ref 请投币);
        //fsc.InitSound("VOC/待机/敲击乐翻天大家一起来暴击小怪兽", ref 敲击乐翻天大家一起来暴击小怪兽);
        fsc.InitSounds("VOC/待机/待机语音{0}", ref 待机语音, 3);
        fsc.InitSounds("VOC/待机/小游戏标题{0}", ref 小游戏标题, 3);

        //----选择中----
        fsc.InitSound("VOC/选择中/单人游戏开始", ref 单人游戏开始);
        fsc.InitSound("VOC/选择中/币数不足请投币", ref 币数不足请投币);
        //fsc.InitSound("VOC/选择中/敲击选择游戏", ref 敲击选择游戏);
        fsc.InitSound("VOC/选择中/等待其他玩家选择", ref 等待其他玩家选择);
        fsc.InitSound("VOC/选择中/等待超时", ref 等待超时);
        fsc.InitSound("VOC/选择中/联机对战乐趣无限", ref 联机对战乐趣无限);
        fsc.InitSound("VOC/选择中/请稍候", ref 请稍候);
        fsc.InitSound("VOC/选择中/请选择游戏模式", ref 请选择游戏模式);
        fsc.InitSounds("VOC/选择中/玩法说明{0}", ref 玩法说明, 3);

        //----游戏中----
        //01提示
        fsc.InitSound("VOC/游戏中/01提示/开始", ref 开始);
        fsc.InitSound("VOC/游戏中/01提示/预备", ref 预备);
        //02鼓励
        fsc.InitSound("VOC/游戏中/02鼓励/不错", ref 不错);
        fsc.InitSound("VOC/游戏中/02鼓励/加油加油", ref 加油加油);
        fsc.InitSound("VOC/游戏中/02鼓励/好样的", ref 好样的);
        //03提示时间
        fsc.InitSound("VOC/游戏中/03提示时间/时间到", ref 时间到);
        fsc.InitSound("VOC/游戏中/03提示时间/时间快到了", ref 时间快到了);
        //04提示_红包游戏
        fsc.InitSound("VOC/游戏中/04提示_红包游戏/哇哦土豪发红包了争取得到最高分", ref 哇哦土豪发红包了争取得到最高分);
        fsc.InitSound("VOC/游戏中/04提示_红包游戏/进入红包游戏", ref 进入红包游戏);
        //05续币提示
        //fsc.InitSound("VOC/游戏中/05续币提示/请敲击选择继续游戏", ref 请敲击选择继续游戏);
        fsc.InitSound("VOC/游戏中/05续币提示/游戏结束", ref 游戏结束);
        //06奖励道具
        fsc.InitSound("VOC/游戏中/06奖励道具/人品爆发了", ref 人品爆发了);
        fsc.InitSound("VOC/游戏中/06奖励道具/恭喜获得宝石", ref 恭喜获得宝石);
        fsc.InitSound("VOC/游戏中/06奖励道具/恭喜获得代币", ref 恭喜获得代币);
        fsc.InitSound("VOC/游戏中/06奖励道具/恭喜获得彩票", ref 恭喜获得彩票);
        //07宝石触发的效果
        fsc.InitSound("VOC/游戏中/07宝石触发的效果/冰雪飞尘", ref 冰雪飞尘);
        fsc.InitSound("VOC/游戏中/07宝石触发的效果/双倍分数", ref 双倍分数);
        fsc.InitSound("VOC/游戏中/07宝石触发的效果/奖励1000分", ref 奖励1000分);
        fsc.InitSound("VOC/游戏中/07宝石触发的效果/时间充值", ref 时间充值);
        fsc.InitSound("VOC/游戏中/07宝石触发的效果/火焰流星", ref 火焰流星);
        fsc.InitSound("VOC/游戏中/07宝石触发的效果/能量锤子", ref 能量锤子);
        //08得到双倍得分或大锤子时
        fsc.InitSound("VOC/游戏中/08得到双倍得分或大锤子时/双倍得分快抓紧时间消灭小怪兽", ref 双倍得分快抓紧时间消灭小怪兽);
        //fsc.InitSound("VOC/游戏中/08得到双倍得分或大锤子时/鸿运当头使劲敲", ref 鸿运当头使劲敲);
        //09扣时间时提示
        fsc.InitSound("VOC/游戏中/09扣时间时提示/小心时间减少了", ref 小心时间减少了);


        //----游戏玩法提示----
        //敲击守护员
        fsc.InitSound("VOC/游戏玩法提示/敲击守护员/注意别让怪物越过防御线", ref 注意别让怪物越过防御线);
        fsc.InitSound("VOC/游戏玩法提示/敲击守护员/加油努力消灭分裂出来的小怪", ref 加油努力消灭分裂出来的小怪);
        //暴击机械怪
        //fsc.InitSound("VOC/游戏玩法提示/暴击机械怪/大怪冒头了赶紧敲击", ref 大怪冒头了赶紧敲击);
        fsc.InitSound("VOC/游戏玩法提示/暴击机械怪/注意怪物冒头时才能打到它哦", ref 注意怪物冒头时才能打到它哦);
        //轰击敲敲萌
        fsc.InitSound("VOC/游戏玩法提示/轰击敲敲萌/砰砰砰出来的怪物一个都别放过", ref 砰砰砰出来的怪物一个都别放过);
        //fsc.InitSound("VOC/游戏玩法提示/轰击敲敲萌/敲击炮台轰炸怪物", ref 敲击炮台轰炸怪物);


        //----结算----
        //提示
        fsc.InitSound("VOC/结算/提示/还要再来玩哦", ref 还要再来玩哦);
        //提示_联机比赛
        fsc.InitSound("VOC/结算/提示_联机比赛/领先了真厉害", ref 领先了真厉害);
        fsc.InitSound("VOC/结算/提示_联机比赛/落后了加油加油", ref 落后了加油加油);
        fsc.InitSound("VOC/结算/提示_联机比赛/平局双方不分上下呢", ref 平局双方不分上下呢);
        fsc.InitSound("VOC/结算/提示_联机比赛/恭喜获胜", ref 恭喜获胜);
        fsc.InitSound("VOC/结算/提示_联机比赛/真可惜继续加油哦", ref 真可惜继续加油哦);
        //提示_转盘
        //fsc.InitSound("VOC/结算/提示_转盘/请敲击停止转盘", ref 请敲击停止转盘);
        //结束
        fsc.InitSound("VOC/结算/结束/欢迎下次挑战", ref 欢迎下次挑战);

        //----jp大奖----
        //提示
        fsc.InitSound("VOC/jp大奖/提示/真走运集齐宝石进入终极大奖", ref 真走运集齐宝石进入终极大奖);
        //fsc.InitSound("VOC/jp大奖/提示/敲中星星会有额外奖励哦", ref 敲中星星会有额外奖励哦);
        //fsc.InitSound("VOC/jp大奖/提示/请敲击屏幕开始抽奖", ref 请敲击屏幕开始抽奖);
        //敲中星星时奖励票数
        fsc.InitSound("VOC/jp大奖/敲中星星时奖励票数/哇哦额外奖励100票", ref 哇哦额外奖励100票);
        fsc.InitSound("VOC/jp大奖/敲中星星时奖励票数/人品爆发啦额外奖励1000票", ref 人品爆发啦额外奖励1000票);
    }
}