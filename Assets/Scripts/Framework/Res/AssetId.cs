using System;

namespace ZXC
{
    public class AssetId
    {
        public int PackId { get; private set; }

        public int GroupId { get; private set; }

        public int ResId { get; private set; }

        /// <summary>
        /// 创建AssetId的工厂方法（未使用Pool，而是直接new）
        /// </summary>
        /// <param name="packId"></param>
        /// <param name="groupId"></param>
        /// <param name="resId"></param>
        /// <returns></returns>
        public static AssetId Create(int packId, int groupId, int resId)
        {
            return new AssetId(packId, groupId, resId);
        }

        public AssetId(int packId, int groupId, int resId)
        {
            PackId = packId;
            GroupId = groupId;
            ResId = resId;
        }

        /// <summary>
        /// 格式化输出
        /// </summary>
        /// <returns>P/G/R</returns>
        public override string ToString()
        {
            return PackId + "/" + GroupId + "/" + ResId;
        }

        public override bool Equals(object obj)
        {
            if(obj is AssetId)
            {
                return this == obj as AssetId;
            }
            else
            {
                throw new InvalidCastException("the args obj is not a assetId");
            }
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public static bool operator == (AssetId lhs, AssetId rhs)
        {
            return lhs.PackId == rhs.PackId &&
                lhs.GroupId == rhs.GroupId && 
                lhs.ResId == rhs.ResId;
        }

        public static bool operator != (AssetId lhs, AssetId rhs)
        {
            return lhs.PackId != rhs.PackId || 
                lhs.GroupId != rhs.GroupId || 
                lhs.ResId == rhs.ResId;
        }
    }
}