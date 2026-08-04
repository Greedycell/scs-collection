namespace UCS.Files.CSV_Helpers
{
    public static class GlobalID
    {
        private const int Reference = 1125899907;

        public static int CreateGlobalID(int _Index, int _Count)
        {
            return _Count + 1000000 * _Index;
        }

        public static int GetClassID(int _Type)
        {
            /*
             * Resource:
             * commandeType: 3000000 (2DC6C0)
             * commandType: 786432
             * return 3 + 0
             */
            _Type = (int)((Reference * (long)_Type) >> 32);
            return (_Type >> 18) + (_Type >> 31);
        }

        public static int GetInstanceID(int _GlobalID)
        {
            /*
             * Resource:
             * globalID: 3000000 (2DC6C0)
             * r1: 1125899907
             * r1: 786432
             * return 3000000 - 1000000 * (3 + 0)
             */
            int _ReferenceT = 0;
            _ReferenceT     = (int)((Reference * (long) _GlobalID) >> 32);
            return _GlobalID - 1000000 * ((_ReferenceT >> 18) + (_ReferenceT >> 31));
        }
    }
}