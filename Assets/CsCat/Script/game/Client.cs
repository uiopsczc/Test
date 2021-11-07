using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

namespace CsCat
{
    public class Client : TickObject, ISingleton
    {
        public MoveManager moveManager;

        //管理模块
        public AssetBundleManager assetBundleManager;
        public AssetBundleUpdater assetBundleUpdater;
        public AudioManager audioManager;

        public PhysicsManager physicsManager;
        public CommandManager commandManager = new CommandManager();
        public GameHotKeyManager gameHotKeyManager;

        public FrameCallbackMananger frameCallbackMananger = new FrameCallbackMananger();
        public GuidManager guidManager;

        public RandomManager randomManager = new RandomManager();

//    public RedDotManager redDotManager;
        public CfgManager cfgManager;


        public RPN rpn = new RPN();
        public CombatBase combat;

        public UserFactory userFactory;
        public RoleFactory roleFactory;
        public ItemFactory itemFactory;
        public MissionFactory missionFactory;
        public DoerEventFactory doerEventFactory;
        public SceneFactory sceneFactory;


//    public RedDotLogic redDotLogic;

        public User user;
        public Role main_role;


        public StageBase stage;
        public SyncUpdate syncUpdate = new SyncUpdate();

        public IdPool idPool = new IdPool();

        //通用模块
        public override TimerManager timerManager => cache.GetOrAddDefault(() => new TimerManager());

        public UIManager uiManager;
        public static Client instance => SingletonFactory.instance.Get<Client>();


        public string language;

        public void SingleInit()
        {
        }

        public void Init(GameObject gameObject)
        {
            base.Init();
            this.graphicComponent.SetGameObject(gameObject, true);
        }

        public override void Start()
        {
            base.Start();
#if !UNITY_EDITOR
      EditorModeConst.Is_Editor_Mode = false;
#endif
            this.moveManager = graphicComponent.gameObject.AddComponent<MoveManager>();

            language = GameData.instance.langData.language;
            guidManager = new GuidManager(GameData.instance.guid_current);
            assetBundleUpdater = AddChild<AssetBundleUpdater>("AssetBundleUpdater");
            assetBundleManager = AddChild<AssetBundleManager>("AssetBundleManager");
            cfgManager = AddChild<CfgManager>("CfgManager");
            audioManager = AddChild<AudioManager>("AudioManager");
            physicsManager = AddChild<PhysicsManager>("physicsManager");
            gameHotKeyManager = AddChild<GameHotKeyManager>("DefaultInputManager");
            uiManager = AddChildWithoutInit<UIManager>("UIManager");
            uiManager.Init();
            uiManager.PostInit();
            uiManager.SetIsEnabled(true, false);

//      redDotManager = AddChild<RedDotManager>("RedDotManager");


            userFactory = AddChild<UserFactory>("UserFactory");
            roleFactory = AddChild<RoleFactory>("RoleFactory");
            missionFactory = AddChild<MissionFactory>("MissionFactory");
            itemFactory = AddChild<ItemFactory>("ItemFactory");
            doerEventFactory = AddChild<DoerEventFactory>("DoerEventFactory");
            sceneFactory = AddChild<SceneFactory>("SceneFactory");


            user = GameData2.instance.RestoreUser();
            TestUser();
            Test();
            Goto<StageShowLogo>();
        }

        public void Test()
        {
            TestProtoTest.Test();
        }


        public void TestUser()
        {
            //    ItemTest.Test();
        }


        public void Goto<T>(float fade_hide_duration = 0f, Action on_stage_show_callback = null)
            where T : StageBase, new()
        {
            StartCoroutine(IEGoto<T>(fade_hide_duration, on_stage_show_callback));
        }

        public IEnumerator IEGoto<T>(float fade_hide_duration = 0f, Action on_stage_show_callback = null)
            where T : StageBase, new()
        {
            if (stage != null)
            {
                if (fade_hide_duration > 0)
                {
                    uiManager.FadeTo(0, 1, fade_hide_duration);

                    yield return new WaitForSeconds(fade_hide_duration);
                }

                yield return stage.IEPreDestroy();
                this.RemoveChild(stage.key);
                stage = null;
            }

            stage = this.AddChild<T>(null);
            stage.on_show_callback = on_stage_show_callback;
            stage.Start();
        }

        //重启
        public void Rebort()
        {
            Goto<StageTest>();
        }

        protected override void _Update(float deltaTime, float unscaledDeltaTime)
        {
            base._Update(deltaTime, unscaledDeltaTime);
            eventDispatchers.Broadcast(GlobalEventNameConst.Update);
            eventDispatchers.Broadcast(GlobalEventNameConst.Update, deltaTime);
            eventDispatchers.Broadcast(GlobalEventNameConst.Update, deltaTime, unscaledDeltaTime);

            this.timerManager.Update(deltaTime, unscaledDeltaTime);
            syncUpdate.Update();
            frameCallbackMananger.Update();
        }

        protected override void _LateUpdate(float deltaTime, float unscaledDeltaTime)
        {
            base._LateUpdate(deltaTime, unscaledDeltaTime);
            eventDispatchers.Broadcast(GlobalEventNameConst.LateUpdate);
            eventDispatchers.Broadcast(GlobalEventNameConst.LateUpdate, deltaTime);
            eventDispatchers.Broadcast(GlobalEventNameConst.LateUpdate, deltaTime, unscaledDeltaTime);

            this.timerManager.LateUpdate(deltaTime, unscaledDeltaTime);
            frameCallbackMananger.LateUpdate();
        }

        protected override void _FixedUpdate(float deltaTime, float unscaledDeltaTime)
        {
            base._FixedUpdate(deltaTime, unscaledDeltaTime);
            eventDispatchers.Broadcast(GlobalEventNameConst.FixedUpdate);
            eventDispatchers.Broadcast(GlobalEventNameConst.FixedUpdate, deltaTime);
            eventDispatchers.Broadcast(GlobalEventNameConst.FixedUpdate, deltaTime, unscaledDeltaTime);

            this.timerManager.FixedUpdate(deltaTime, unscaledDeltaTime);
            frameCallbackMananger.FixedUpdate();
        }

        public void OnApplicationQuit()
        {
            GameData.instance.quit_time_ticks = DateTimeUtil.NowTicks();
            GameData.instance.Save();
            GameData2.instance.Save();
        }

        public void OnApplicationPause(bool is_paused)
        {
        }

        
    }
}