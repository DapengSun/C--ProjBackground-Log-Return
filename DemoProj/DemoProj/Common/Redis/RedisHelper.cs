using Newtonsoft.Json;
using ServiceStack.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Redis
{
    public class RedisHelper : RedisOperatorBase
    {
        public RedisHelper() : base() { }

        #region Hash类型操作
        /// <summary>
        /// 判断某个数据是否已经被缓存
        /// </summary>
        public bool Exist<T>(string hashId, string key)
        {
            return redisClient.HashContainsEntry(hashId, key);
        }
        /// <summary>
        /// 存储数据到hash表
        /// </summary>
        public bool Set(string hashId, string key, Object t)
        {
            var value = Common.Convert.JsonConvert(t);
            return redisClient.SetEntryInHash(hashId, key, value);
        }
        /// <summary>
        /// 移除hash中的某值
        /// </summary>
        public bool Remove(string hashId, string key)
        {
            return redisClient.RemoveEntryFromHash(hashId, key);
        }
        /// <summary>
        /// 移除整个hash
        /// </summary>
        public bool Remove(string key)
        {
            return redisClient.Remove(key);
        }
        /// <summary>
        /// 从hash表获取数据
        /// </summary>
        public T Get<T>(string hashId, string key)
        {
            string value = redisClient.GetValueFromHash(hashId, key);
            return  Common.Convert.ObjectConvert<T>(value);
        }
        /// <summary>
        /// 获取整个hash的数据
        /// </summary>
        public List<T> GetAll<T>(string hashId)
        {
            var result = new List<T>();
            var list = redisClient.GetHashValues(hashId);
            if (list != null && list.Count > 0)
            {
                list.ForEach(x =>
                {
                    var value = Common.Convert.ObjectConvert<T>(x);
                    result.Add(value);
                });
            }
            return result;
        }
        /// <summary>
        /// 设置缓存过期
        /// </summary>
        public void SetExpire(string key, DateTime datetime)
        {
            redisClient.ExpireEntryAt(key, datetime);
        }

        #endregion

        //#region List类型操作
        ///// <summary>
        ///// 从List类型数据指定索引处获取数据，支持正索引和负索引
        ///// </summary>
        ///// <param name="List">列表</param>
        //public string GetItemFromList(string List, int Index)
        //{
        //    return redisClient.GetItemFromList(List, Index);
        //}

        ///// <summary>
        ///// 向列表底部（右侧）批量添加数据
        ///// </summary>
        ///// <param name="list">列表</param>
        //public void AddRangeToList(string List, List<string> Values)
        //{
        //    TimeSpan Ts = new TimeSpan(1000);
        //    redisClient.AddRangeToList(List, Values);
        //    //Hash_SetExpireTimeSpan(redisClient, List, Ts);
        //}

        ///// <summary>
        ///// 获得List中所有成员
        ///// </summary>
        ///// <param name="List">列表</param>
        //public List<string> GetAllItems(string List)
        //{
        //    return redisClient.GetAllItemsFromList(List);
        //}

        ///// <summary>
        ///// 向List列表中添加成员，添加到列表顶部（左侧）
        ///// </summary>
        ///// <param name="List">列表</param>
        ///// <param name="Item">成员</param>
        //public void AddItemToListLeft(string List, string Item)
        //{
        //    redisClient.LPush(List, Encoding.Default.GetBytes(Item));
        //}

        ///// <summary>
        ///// 向List列表中添加成员，添加到列表底部（右侧）
        ///// </summary>
        ///// <param name="List">列表</param>
        ///// <param name="Item">成员</param>
        //public void AddItemToListRight(string List, string Item)
        //{
        //    redisClient.AddItemToList(List, Item);
        //    //redisClient.PushItemToList(List, Item);
        //}

        //#endregion

        //#region Set类型操作
        ///// <summary>
        ///// 向集合中添加数据
        ///// </summary>
        ///// <param name="Item">成员</param>
        ///// <param name="Set">集合</param>
        //public void GetItemToSet(string Item, string Set)
        //{
        //    redisClient.AddItemToSet(Set, Item);
        //}

        ///// <summary>
        ///// 获取集合中所有成员
        ///// </summary>
        ///// <param name="Set"></param>
        ///// <returns></returns>
        //public HashSet<string> GetAllItemsFromSet(string Set)
        //{
        //    return redisClient.GetAllItemsFromSet(Set);
        //}

        ///// <summary>
        ///// 获取FormSet集合与其他集合不同的成员
        ///// </summary>
        ///// 1． params只能用于一维数组，不能用于多维数组和诸如ArrayList、List等任何类似于数组的集合类型。 
        ///// 2． 被加上params关键字的形参，必须是形参列表中最后一个形参，并且方法声明中只允许一个 params 关键字。
        ///// <param name="FromSet"></param>
        ///// <param name="toSet"></param>
        ///// <returns></returns>
        //public HashSet<string> GetSetDiff(string fromSet, params string[] toSet)
        //{
        //    return redisClient.GetDifferencesFromSet(fromSet, toSet);
        //}

        ///// <summary>
        ///// 获取集合的交集
        ///// </summary>
        ///// <param name="set"></param>
        ///// <returns></returns>
        //public HashSet<string> GetSetInter(params string[] set)
        //{
        //    return redisClient.GetIntersectFromSets(set);
        //}

        ///// <summary>
        ///// 获取集合的并集
        ///// </summary>
        ///// <param name="set"></param>
        ///// <returns></returns>
        //public HashSet<string> GetSetUnion(params string[] set)
        //{
        //    return redisClient.GetUnionFromSets(set);
        //}

        ///// <summary>
        ///// 向有序集合中添加有效成员
        ///// </summary>
        ///// <param name="set"></param>
        ///// <param name="value"></param>
        ///// <param name="score"></param>
        //public void AddItemToSortedSet(string set, string value, long score)
        //{
        //    redisClient.AddItemToSortedSet(set, value, score);
        //}

        ///// <summary>
        ///// 获取某个值在集合中的排名,按分数的降序排列
        ///// </summary>
        ///// <param name="set"></param>
        ///// <param name="value"></param>
        ///// <returns></returns>
        //public int GetItemIndexInSortedSetDesc(string set, string value)
        //{
        //    int index = int.Parse(redisClient.GetItemIndexInSortedSetDesc(set, value).ToString());
        //    return index;
        //}

        ///// <summary>
        ///// 获取某个值在集合中的排名,按分数的升序排列
        ///// </summary>
        ///// <param name="set"></param>
        ///// <param name="value"></param>
        ///// <returns></returns>
        //public int GetItemIndexInSortedSet(string set, string value)
        //{
        //    int index = int.Parse(redisClient.GetItemIndexInSortedSet(set, value).ToString());
        //    return index;
        //}

        ///// <summary>
        ///// 获得有序集合中某个值得分数
        ///// </summary>
        ///// <param name="set"></param>
        ///// <param name="value"></param>
        ///// <returns></returns>
        //public double GetItemScoreInSortedSet(string set, string value)
        //{
        //    return redisClient.GetItemScoreInSortedSet(set, value);
        //}

        ///// <summary>
        ///// 获得有序集合中，某个排名范围的所有值
        ///// </summary>
        ///// <param name="set"></param>
        ///// <param name="value"></param>
        ///// <returns></returns>
        //public List<string> GetRangeFromSortedSet(string set, int beginRank, int endRank)
        //{
        //    return redisClient.GetRangeFromSortedSet(set, beginRank, endRank);
        //}

        ///// <summary>
        ///// 获得有序集合中，某个分数范围内的所有值，升序
        ///// </summary>
        ///// <param name="set"></param>
        ///// <param name="beginScore"></param>
        ///// <param name="endScore"></param>
        //public List<string> GetRangeFromSortedSet(string set, double beginScore, double endScore)
        //{
        //    return redisClient.GetRangeFromSortedSetByHighestScore(set, beginScore, endScore);
        //}

        ///// <summary>
        ///// 获得有序集合中，某个分数范围内的所有值，降序
        ///// </summary>
        ///// <param name="set"></param>
        ///// <param name="value"></param>
        ///// <returns></returns>
        //public List<string> GetRangeFromSortedSetDesc(string set, double beginScore, double endScore)
        //{
        //    return redisClient.GetRangeFromSortedSetByLowestScore(set, beginScore, endScore);
        //}
        //#endregion

        //#region String类型操作
        ///// <summary>
        ///// 根据Key键值,获得Byte[]类型的值
        ///// </summary>
        ///// <param name="Key">键值</param>
        //public byte[] getValueByte(string Key)
        //{
        //    return redisClient.Get(Key);
        //}

        ///// <summary>
        ///// 根据Key键值,获得string类型的值
        ///// </summary>
        ///// <param name="Key">键值</param>
        //public string GetValueString(string Key)
        //{
        //    return redisClient.GetValue(Key);
        //}

        //#region 设置过期时间
        ///// <summary>
        ///// 过期时间设置
        ///// </summary>
        ///// <param name="key"></param>
        ///// <param name="datetime"></param>
        //public void Hash_SetExpireDateTime(RedisClient redis, string key, DateTime dateTime)
        //{
        //    redis.ExpireEntryAt(key, dateTime);
        //}

        ///// <summary>
        ///// 过期时间间隔设置
        ///// </summary>
        ///// <param name="key"></param>
        ///// <param name="datetime"></param>
        //public void Hash_SetExpireTimeSpan(RedisClient redis, string key, TimeSpan timeSpan)
        //{
        //    redis.ExpireEntryIn(key, timeSpan);
        //}
        //#endregion

        //#endregion
    }
}
