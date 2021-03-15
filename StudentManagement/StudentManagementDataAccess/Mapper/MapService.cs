using AutoMapper;
using System.Collections.Generic;
using System.Linq;

namespace Model.Mapper
{
    /// <summary>
    /// 使用TinyMapper作為資料表Map工具
    /// </summary>
    public static class MapService
    {
        /// <summary>
        /// Map
        /// </summary>
        /// <typeparam name="Copy">複製型別</typeparam>
        /// <typeparam name="Paste">貼上型別</typeparam>
        /// <param name="copyData">複製資料</param>
        /// <returns>貼上資料</returns>
        public static Paste Map<Copy, Paste>(this Copy copyData)
        {
            Paste paste = default(Paste);

            try
            {
                var config = new MapperConfiguration(cfg => cfg.CreateMap<Copy, Paste>());

                var mapper = config.CreateMapper();

                paste = mapper.Map<Copy, Paste>(copyData);
            }
            catch
            {
              
            }

            return paste;
        }

        /// <summary>
        /// Map
        /// </summary>
        /// <typeparam name="Copy">複製型別集合</typeparam>
        /// <typeparam name="Paste">貼上型別集合</typeparam>
        /// <param name="copyData">複製資料集合</param>
        /// <returns>貼上資料集合</returns>
        public static IEnumerable<Paste> Map<Copy, Paste>(this IEnumerable<Copy> copyData)
        {
            IEnumerable<Paste> paste = null;

            try
            {
                var config = new MapperConfiguration(cfg => cfg.CreateMap<Copy, Paste>());

                var mapper = config.CreateMapper();

                paste = mapper.Map<IEnumerable<Copy>, IEnumerable<Paste>>(copyData);
            }
            catch
            {
               
            }

            return paste;
        }

        public static IEnumerable<Paste> ProjectTo<Copy, Paste>(this IQueryable<Copy> copyData)
        {
            IEnumerable<Paste> paste = null;

            var config = new MapperConfiguration(cfg => cfg.CreateMap<Copy, Paste>());

            var mapper = config.CreateMapper();

            paste = mapper.ProjectTo<Paste>(copyData);

            return paste;
        }

        /// <summary>
        /// TinyMapper.ProjectTo 針對 IQueryable
        /// </summary>
        /// <typeparam name="Copy">複製型別集合</typeparam>
        /// <typeparam name="Paste">貼上型別集合</typeparam>
        /// <param name="copyData">複製資料集合</param>
        /// <returns>貼上資料集合</returns>
        public static IQueryable<Paste> Map<Copy, Paste>(this IQueryable<Copy> copyData)
        {
            IQueryable<Paste> paste = null;

            var config = new MapperConfiguration(cfg => cfg.CreateMap<Copy, Paste>());

            var mapper = config.CreateMapper();

            paste = mapper.ProjectTo<Paste>(copyData);
            return paste;
        }
    }
}