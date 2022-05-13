using System;
using System.Collections.Generic;
using System.Linq;
using Codice.CM.SEIDInfo;
using Cysharp.Threading.Tasks;
using Game.DataBase.PlayerDataBase;
using Game.Scripts.Damage;
using Game.Scripts.Damage.AbState;
using Game.Scripts.Player.Skill;
using Sirenix.OdinInspector;
using UniRx;
using UnityEngine;

namespace Game.Scripts.Player
{
    public class PlayerCore : MonoBehaviour, IDamageApplicable
    {
        public PlayerType Type => _type;
        private PlayerType _type;

        public IReadOnlyReactiveProperty<float> MP => _mp;
        private FloatReactiveProperty _mp = new FloatReactiveProperty();

        public IReadOnlyReactiveProperty<float> SP => _sp;
        private FloatReactiveProperty _sp = new FloatReactiveProperty();

        public int Exp { get; set; }

        [ReadOnly] public int RequiredExp => (int) (12 + 5 * Math.Pow(_currentParameter.Level - 1, 2.6));

        public bool IsAlive { get; private set; }

        public IObservable<Unit> OnInitAsync => _onInitAsyncSubject;
        private AsyncSubject<Unit> _onInitAsyncSubject = new AsyncSubject<Unit>();

        public IObservable<Damage.Damage> OnDamage => _onDamageSubject;
        private Subject<Damage.Damage> _onDamageSubject = new Subject<Damage.Damage>();

        public IObservable<Damage.Damage> OnDead => _onDeadSubject;
        private Subject<Damage.Damage> _onDeadSubject = new Subject<Damage.Damage>();

        public IObservable<int> OnLevelUp => _onLevelUpSubject;
        private Subject<int> _onLevelUpSubject = new Subject<int>();

        public BaseParameter CurrentParameter => _currentParameter;
        private PlayerParameters _currentParameter;

        private PlayerParameters _initParameter;

        public PlayerInventory Inventory => _inventory;
        private PlayerInventory _inventory = new PlayerInventory();

        public PlayerEquipment Equipment => _equipment;
        private PlayerEquipment _equipment;

        public PlayerAttack Attacker => _attacker;
        private PlayerAttack _attacker;

        public PlayerSkill Skill => _skill;
        private PlayerSkill _skill;

        public Dictionary<Type, IAbState> AbStates => _abStates;
        private Dictionary<Type, IAbState> _abStates = new Dictionary<Type, IAbState>();

        public SkillDataBase SkillDataBase => _skillDataBase;
        private SkillDataBase _skillDataBase;

        public PlayerCore()
        {
            _attacker = new PlayerAttack(this);
        }

        private void Awake()
        {
            _onInitAsyncSubject.Subscribe(_ =>
            {
                OnDamage
                    .Subscribe(x =>
                    {
                        _currentParameter.HP.Value -= x.Value;

                        if (x.AbState != null)
                        {
                            if (!_abStates.ContainsKey(x.AbState.GetType()))
                            {
                                _abStates.Add(x.AbState.GetType(), x.AbState);
                            }
                            else
                            {
                                _abStates[x.AbState.GetType()].Dispose();
                                _abStates[x.AbState.GetType()] = x.AbState;
                            }

                            x.AbState.Activate();
                        }

                        if (_currentParameter.HP.Value <= 0)
                        {
                            _onDeadSubject.OnNext(x);
                        }
                    }).AddTo(this);

                OnDead
                    .Subscribe(_ => { IsAlive = false; }).AddTo(this);
            }).AddTo(this);
        }

        public void Init(PlayerJobData data)
        {
            _currentParameter = (PlayerParameters) data.parameters.Copy();
            _initParameter = (PlayerParameters) _currentParameter.Copy();

            _currentParameter.HP.Value = _currentParameter.MaxHP.BaseValue;
            _mp.Value = _currentParameter.MaxMP.BaseValue;
            _sp.Value = _currentParameter.MaxSP.BaseValue;

            IsAlive = true;

            _equipment = new PlayerEquipment(this.transform);
            _skill = new PlayerSkill(this);

            _skillDataBase = Resources.Load<SkillDataBase>("SkillDataBase");

            _onInitAsyncSubject.OnNext(Unit.Default);
            _onInitAsyncSubject.OnCompleted();
        }

        public void SetXP(PlayerStatusType healType, float value)
        {
            switch (healType)
            {
                case PlayerStatusType.HP:
                    _currentParameter.HP.Value += value;
                    break;
                case PlayerStatusType.MP:
                    _mp.Value += value;
                    break;
                case PlayerStatusType.SP:
                    _sp.Value += value;
                    break;
            }
        }

        public void ApplyDamage(Damage.Damage damage)
        {
            _onDamageSubject.OnNext(damage);
        }

        public void LevelUp()
        {
            var nowLv = ++_currentParameter.Level;

            _currentParameter.MaxHP.BaseValue += (int) (_currentParameter.dMaxHP * nowLv + 3);
            _currentParameter.MaxMP.BaseValue += (int) (_currentParameter.dMaxMP * nowLv + 3);
            _currentParameter.MaxSP.BaseValue += (int) (_currentParameter.dMaxSP * nowLv + 3);
            _currentParameter.STR.BaseValue += (int) (_currentParameter.dSTR * nowLv + 3);
            _currentParameter.DEF.BaseValue += (int) (_currentParameter.dDEF * nowLv + 3);
            _currentParameter.INT.BaseValue += (int) (_currentParameter.dINT * nowLv + 3);
            
            _onLevelUpSubject.OnNext(nowLv);
        }

        private void OnDestroy()
        {
            _onDamageSubject.Dispose();

            foreach (var x in AbStates)
            {
                x.Value.Dispose();
            }
        }
    }
}