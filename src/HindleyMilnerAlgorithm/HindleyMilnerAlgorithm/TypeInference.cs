using System;
using System.Collections.Generic;

namespace HindleyMilnerAlgorithm
{
    public class Substitution
    {
        public Dictionary<string, HMType> Mappings { get; set; }
        public bool Failed { get; set; }

        public Substitution()
        {
            Mappings = new Dictionary<string, HMType>();
            Failed = false;
        }

        public HMType Apply(HMType type)
        {
            if (type is TypeVariable)
            {
                TypeVariable tv = (TypeVariable)type;
                if (Mappings.ContainsKey(tv.Name))
                {
                    return Apply(Mappings[tv.Name]);
                }

                return tv;
            }
            else if (type is TypeOperator)
            {
                TypeOperator top = (TypeOperator)type;
                List<HMType> newTypes = new List<HMType>();
                foreach (HMType t in top.Types)
                {
                    newTypes.Add(Apply(t));
                }

                return new TypeOperator(top.Name, newTypes);
            }

            return type;
        }

        public Scheme Apply(Scheme scheme)
        {
            return new Scheme(scheme.TypeVariables, Apply(scheme.Type));
        }

        public void Compose(Substitution other)
        {
            if (this.Failed || other.Failed)
            {
                this.Failed = true;
                return;
            }

            List<string> keys = new List<string>(Mappings.Keys);
            foreach (string key in keys)
            {
                Mappings[key] = other.Apply(Mappings[key]);
            }

            foreach (KeyValuePair<string, HMType> pair in other.Mappings)
            {
                if (!Mappings.ContainsKey(pair.Key))
                {
                    Mappings[pair.Key] = pair.Value;
                }
            }
        }
    }

    public static class TypeInference
    {
        private static int count = 0;
        public static HMType NewTypeVariable()
        {
            count = count + 1;
            return new TypeVariable("t" + count.ToString());
        }

        public static Substitution Unify(HMType t1, HMType t2)
        {
            if (t1 is TypeVariable)
            {
                return UnifyVar((TypeVariable)t1, t2);
            }

            if (t2 is TypeVariable)
            {
                return UnifyVar((TypeVariable)t2, t1);
            }

            if (t1 is TypeOperator && t2 is TypeOperator)
            {
                TypeOperator o1 = (TypeOperator)t1;
                TypeOperator o2 = (TypeOperator)t2;
                if (o1.Name != o2.Name || o1.Types.Count != o2.Types.Count)
                {
                    Substitution fail = new Substitution();
                    fail.Failed = true;
                    return fail;
                }
                else
                {
                    Substitution s1 = new Substitution();
                    for (int i = 0; i < o1.Types.Count; i++)
                    {
                        Substitution si = Unify(s1.Apply(o1.Types[i]), s1.Apply(o2.Types[i]));
                        if (si.Failed)
                        {
                            Substitution fail = new Substitution();
                            fail.Failed = true;
                            return fail;
                        }

                        s1.Compose(si);
                        if (s1.Failed)
                        {
                            return s1;
                        }
                    }

                    return s1;
                }
            }

            Substitution empty = new Substitution();
            return empty;
        }

        private static Substitution UnifyVar(TypeVariable tv, HMType t)
        {
            Substitution s = new Substitution();
            if (t is TypeVariable && ((TypeVariable)t).Name == tv.Name)
            {
                return s;
            }

            if (t.FreeTypeVars().Contains(tv.Name))
            {
                s.Failed = true;
                return s;
            }

            s.Mappings[tv.Name] = t;
            return s;
        }

        public static Tuple<Substitution, HMType> AlgorithmW(Dictionary<string, Scheme> env, Expr expr)
        {
            if (expr is Var)
            {
                Var v = (Var)expr;
                if (!env.ContainsKey(v.Name))
                {
                    Substitution fail = new Substitution();
                    fail.Failed = true;
                    return new Tuple<Substitution, HMType>(fail, new TypeVariable(v.Name));
                }

                Scheme scheme = env[v.Name];
                HMType tInst = Instantiate(scheme);
                return new Tuple<Substitution, HMType>(new Substitution(), tInst);
            }
            else if (expr is Lam)
            {
                Lam lam = (Lam)expr;
                HMType tv = NewTypeVariable();
                Dictionary<string, Scheme> newEnv = new Dictionary<string, Scheme>(env);
                newEnv[lam.Param] = new Scheme(new List<string>(), tv);
                Tuple<Substitution, HMType> result = AlgorithmW(newEnv, lam.Body);
                if (result.Item1.Failed)
                {
                    return result;
                }

                Substitution s = result.Item1;
                HMType bodyType = result.Item2;
                HMType funType = new TypeOperator("->", new List<HMType> { s.Apply(tv), bodyType });
                return new Tuple<Substitution, HMType>(s, funType);
            }
            else if (expr is App)
            {
                App app = (App)expr;
                Tuple<Substitution, HMType> r1 = AlgorithmW(env, app.Function);
                if (r1.Item1.Failed)
                {
                    return r1;
                }

                Substitution s1 = r1.Item1;
                HMType t1 = r1.Item2;
                Tuple<Substitution, HMType> r2 = AlgorithmW(ApplySubstEnv(s1, env), app.Argument);
                if (r2.Item1.Failed)
                {
                    return r2;
                }

                Substitution s2 = r2.Item1;
                HMType t2 = r2.Item2;
                HMType tv = NewTypeVariable();
                Substitution s3 = Unify(s2.Apply(t1), new TypeOperator("->", new List<HMType> { t2, tv }));
                if (s3.Failed)
                {
                    return new Tuple<Substitution, HMType>(s3, tv);
                }

                s2.Compose(s3);
                s1.Compose(s2);
                return new Tuple<Substitution, HMType>(s1, s3.Apply(tv));
            }
            else if (expr is Let)
            {
                Let let = (Let)expr;
                Tuple<Substitution, HMType> r1 = AlgorithmW(env, let.Definition);
                if (r1.Item1.Failed)
                {
                    return r1;
                }

                Substitution s1 = r1.Item1;
                HMType t1 = r1.Item2;
                Dictionary<string, Scheme> env1 = ApplySubstEnv(s1, env);
                Scheme scheme = Generalize(env1, t1);
                Dictionary<string, Scheme> newEnv = new Dictionary<string, Scheme>(env1);
                newEnv[let.VarName] = scheme;
                Tuple<Substitution, HMType> r2 = AlgorithmW(newEnv, let.Body);
                if (r2.Item1.Failed)
                {
                    return r2;
                }

                Substitution s2 = r2.Item1;
                HMType t2 = r2.Item2;
                s1.Compose(s2);
                return new Tuple<Substitution, HMType>(s1, t2);
            }

            return new Tuple<Substitution, HMType>(new Substitution(), NewTypeVariable());
        }

        private static Dictionary<string, Scheme> ApplySubstEnv(Substitution s, Dictionary<string, Scheme> env)
        {
            Dictionary<string, Scheme> newEnv = new Dictionary<string, Scheme>();
            foreach (KeyValuePair<string, Scheme> pair in env)
            {
                newEnv[pair.Key] = s.Apply(pair.Value);
            }

            return newEnv;
        }

        private static Scheme Generalize(Dictionary<string, Scheme> env, HMType type)
        {
            HashSet<string> envFv = new HashSet<string>();
            foreach (KeyValuePair<string, Scheme> pair in env)
            {
                envFv.UnionWith(pair.Value.FreeTypeVars());
            }

            HashSet<string> typeFv = type.FreeTypeVars();
            typeFv.ExceptWith(envFv);
            List<string> vars = new List<string>(typeFv);
            return new Scheme(vars, type);
        }

        private static HMType Instantiate(Scheme scheme)
        {
            Dictionary<string, HMType> substMapping = new Dictionary<string, HMType>();
            foreach (string var in scheme.TypeVariables)
            {
                substMapping[var] = NewTypeVariable();
            }

            Substitution s = new Substitution();
            s.Mappings = substMapping;
            return s.Apply(scheme.Type);
        }
    }
}