using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    public interface IRedisHelper:IDisposable
    {
        /* Author:Sdp
             * Date:2016.11.07
             * 封装servicestack.redis
             * 根据数据类型 添加CURD操作 
             * redis是一个典型的k/v型数据库，redis主要类型：Hash、List、string、Set、ZSet
             * 1、string是最简单的字符串类型
             * 
             * 2、list是字符串列表，其内部是用双向链表实现的，因此在获取/设置数据时可以支持正负索引
             * 也可以将其当做堆栈结构使用
             * 
             * 3、hash类型是一种字典结构，也是最接近RDBMS的数据类型，其存储了字段和字段值的映射，但字段值只能是
             * 字符串类型，散列类型适合存储对象，建议使用对象类别和ID构成键名，使用字段表示对象属性，字
             * 段值存储属性值，例如：car:2 price 500 ,car:2  color black,用redis命令设置散列时，命令格式
             * 如下：HSET key field value，即key，字段名，字段值
             * 
             * 4、set是一种集合类型，redis中可以对集合进行交集，并集和互斥运算
             *
             * 5、sorted set是在集合的基础上为每个元素关联了一个“分数”，我们能够
             * 获得分数最高的前N个元素，获得指定分数范围内的元素，元素是不同的，但是"分数"可以是相同的
             * set是用散列表和跳跃表实现的，获取数据的速度平均为o(log(N))
             * 
             * 需要注意的是，redis所有数据类型都不支持嵌套
             * redis中一般不区分插入和更新操作，只是命令的返回值不同
             * 在插入key时，如果不存在，将会自动创建
             * 
             * 在实际生产环境中，由于多线程并发的关系，建议使用连接池，本类只是用于测试简单的数据类型
        */

        

        /// <summary>
        /// 创建RedisClient连接
        /// </summary>
        /// <param name="HostIP">主机IP</param>
        /// <param name="HostPort">主机端口号</param>
        /// <param name="Keyword">密码</param>
        //void CreateClient(string HostIP, int HostPort, string Keyword);

        /// <summary>
        /// 根据Key键值,获得string类型的值
        /// </summary>
        /// <param name="Key">键值</param>
        /// <returns></returns>
        string GetValueString(string Key);

        /// <summary>
        /// 根据Key键值,获得Byte[]类型的值
        /// </summary>
        /// <param name="Key">键值</param>
        /// <returns></returns>
        byte[] getValueByte(string Key);

        /// <summary>
        /// 存储对象T t到hash集合中
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="t"></param>
        void StoreAsHash<T>(T t);

        /// <summary>
        /// 判断Hashid数据集中是否存在key的数据
        /// </summary>
        /// <param name="Hashid"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        bool HashContainsEntry(string Hashid, string key);

        /// <summary>
        /// 获取对象T中ID为id的数据
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="id"></param>
        /// <returns></returns>
        T GetFromHash<T>(object id);

        /// <summary>
        /// 获得Hash类型的所有字段
        /// </summary>
        /// <param name="HashId">HashId键值</param>
        /// <returns></returns>
        List<string> GetHashKeys(string HashId);

        /// <summary>
        /// 获得Hash类型的所有值
        /// </summary>
        /// <param name="HashId">HashId键值</param>
        /// <returns></returns>
        List<string> GetHashValues(string HashId);

        /// <summary>
        /// 获得hash型key某个字段的值
        /// </summary>
        /// <param name="Key">键值</param>
        /// <param name="Field">字段</param>
        string GetValueFromHash(string Key, string Field);

        /// <summary>
        /// 设置Hash型Key某个字段的值
        /// </summary>
        /// <param name="Key">键值</param>
        /// <param name="Field">字段</param>
        void SetHashFieldValue(string Key, string Field, string Value);

        /// <summary>
        /// 如果HashId集合中存在key/value则不添加返回false，如果不存在在添加key/value,返回true
        /// </summary>
        /// <param name="HashId"></param>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        bool SetEntryInHashIfNotExists(string HashId, string key, string value);

        /// <summary>
        /// 获取所有hashid数据集的key/value数据集合
        /// </summary>
        /// <param name="HashId"></param>
        /// <returns></returns>
        Dictionary<string, string> GetAllEntriesFromHash(string HashId);

        /// <summary>
        /// 获取hashid数据集中的数据总数
        /// </summary>
        /// <param name="HashId"></param>
        /// <returns></returns>
        long GetHashCount(string HashId);

        /// <summary>
        /// 向List列表中添加成员，添加到列表底部（右侧）
        /// </summary>
        /// <param name="List">列表</param>
        /// <param name="Item">成员</param>
        void AddItemToListRight(string List, string Item);

        /// <summary>
        /// 向List列表中添加成员，添加到列表顶部（左侧）
        /// </summary>
        /// <param name="List">列表</param>
        /// <param name="Item">成员</param>
        void AddItemToListLeft(string List, string Item);

        /// <summary>
        /// 获得List中所有成员
        /// </summary>
        /// <param name="List">列表</param>
        /// <returns>List<string></returns>
        List<string> GetAllItems(string List);

        /// <summary>
        /// 从List类型数据指定索引处获取数据，支持正索引和负索引
        /// </summary>
        /// <param name="List">列表</param>
        string GetItemFromList(string List, int Index);

        /// <summary>
        /// 向列表底部（右侧）批量添加数据
        /// </summary>
        /// <param name="list">列表</param>
        /// <param name="values"></param>
        void AddRangeToList(string List, List<string> Values);

        /// <summary>
        /// 向集合中添加数据
        /// </summary>
        /// <param name="Item">成员</param>
        /// <param name="Set">集合</param>
        void GetItemToSet(string Item, string Set);

        /// <summary>
        /// 获取集合中所有成员
        /// </summary>
        /// <param name="Set"></param>
        /// <returns></returns>
        HashSet<string> GetAllItemsFromSet(string Set);

        /// <summary>
        /// 获取FormSet集合与其他集合不同的成员
        /// </summary>
        /// 1． params只能用于一维数组，不能用于多维数组和诸如ArrayList、List等任何类似于数组的集合类型。 
        /// 2． 被加上params关键字的形参，必须是形参列表中最后一个形参，并且方法声明中只允许一个 params 关键字。
        /// <param name="FromSet"></param>
        /// <param name="toSet"></param>
        /// <returns></returns>
        HashSet<string> GetSetDiff(string fromSet, params string[] toSet);

        /// <summary>
        /// 获取集合的并集
        /// </summary>
        /// <param name="set"></param>
        /// <returns></returns>
        HashSet<string> GetSetUnion(params string[] set);

        /// <summary>
        /// 获取集合的交集
        /// </summary>
        /// <param name="set"></param>
        /// <returns></returns>
        HashSet<string> GetSetInter(params string[] set);

        /// <summary>
        /// 向有序集合中添加有效成员
        /// </summary>
        /// <param name="set"></param>
        /// <param name="value"></param>
        /// <param name="score"></param>
        void AddItemToSortedSet(string set, string value, long score);

        /// <summary>
        /// 获取某个值在集合中的排名,按分数的降序排列
        /// </summary>
        /// <param name="set"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        int GetItemIndexInSortedSetDesc(string set, string value);

        /// <summary>
        /// 获取某个值在集合中的排名,按分数的升序排列
        /// </summary>
        /// <param name="set"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        int GetItemIndexInSortedSet(string set, string value);

        /// <summary>
        /// 获得有序集合中某个值得分数
        /// </summary>
        /// <param name="set"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        double GetItemScoreInSortedSet(string set, string value);

        /// <summary>
        /// 获得有序集合中，某个排名范围的所有值
        /// </summary>
        /// <param name="set"></param>
        /// <param name="beginRank"></param>
        /// <param name="endRank"></param>
        /// <returns></returns>
        List<string> GetRangeFromSortedSet(string set, int beginRank, int endRank);

        /// <summary>
        /// 获得有序集合中，某个分数范围内的所有值，升序
        /// </summary>
        /// <param name="set"></param>
        /// <param name="beginScore"></param>
        /// <param name="endScore"></param>
        /// <returns></returns>
        List<string> GetRangeFromSortedSet(string set, double beginScore, double endScore);

        /// <summary>
        /// 获得有序集合中，某个分数范围内的所有值，降序
        /// </summary>
        /// <param name="set"></param>
        /// <param name="beginScore"></param>
        /// <param name="endScore"></param>
        /// <returns></returns>
        List<string> GetRangeFromSortedSetDesc(string set, double beginScore, double endScore);
    }
}
