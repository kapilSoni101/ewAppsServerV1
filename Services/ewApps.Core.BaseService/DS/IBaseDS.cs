/* Copyright © 2018 eWorkplace Apps (https://www.eworkplaceapps.com/). All Rights Reserved.
 * Unauthorized copying of this file, via any medium is strictly prohibited
 * Proprietary and confidential
 * 
 * Author: Sanjeev Khanna <skhanna@eworkplaceapps.com>
 * Date: 24 September 2018
 * 
 * Contributor/s: Nitin Jain
 * Last Updated On: 10 October 2018
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace ewApps.Core.BaseService {

    public interface IBaseDS<T> where T : class {

        //IQueryable<T> GetAll();


        //Task<ICollection<T>> GetAllAsync(CancellationToken token = default(CancellationToken));


        int CountAll();


        Task<int> CountAllAsync(CancellationToken token = default(CancellationToken));


        T Get(Guid id);


        Task<T> GetAsync(Guid id, CancellationToken token = default(CancellationToken));


        T Find(Expression<Func<T, bool>> match);


        Task<T> FindAsync(Expression<Func<T, bool>> match, CancellationToken token = default(CancellationToken));


        ICollection<T> FindAll(Expression<Func<T, bool>> match);


        Task<ICollection<T>> FindAllAsync(Expression<Func<T, bool>> match, CancellationToken token = default(CancellationToken));


        T Add(T t);


        Task<T> AddAsync(T t, CancellationToken token = default(CancellationToken));


        T Update(T t, object key);


        Task<T> UpdateAsync(T t, object key, CancellationToken token = default(CancellationToken));


        void Delete(T entity);

        Task DeleteAsync(Guid id, CancellationToken token = default(CancellationToken));

        void Save();

        Task<int> SaveAsync(CancellationToken token = default(CancellationToken));
        void UpdateEntitySystemFields();

        void UpdateSystemFields(T entity, SystemFieldMask includeOptions = SystemFieldMask.None);


        void UpdateSystemFieldsByOpType(T entity, OperationType opType);

        bool PropertyChanged(Guid id, string propertyName);

        PropType GetOrignalValue<PropType>(Guid id, string propertyName);

    }
}
