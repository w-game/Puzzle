
//------------------------------------------------------------------------------
// Copyright (c) 2018-2019 Beijing Bytedance Technology Co., Ltd.
// All Right Reserved.
// Unauthorized copying of this file, via any medium is strictly prohibited.
// Proprietary and confidential.
//------------------------------------------------------------------------------

namespace ByteDance.Union
{
    public sealed class ABUUserConfig
    {
        public bool logEnable; // 是否开启日志输出
    }

    public class Builder
    {
        public Builder SetLogEnable(bool logEnable)
        {
            return this;
        }

        public ABUUserConfig Build()
        {
            return new ABUUserConfig();
        }
    }
}
