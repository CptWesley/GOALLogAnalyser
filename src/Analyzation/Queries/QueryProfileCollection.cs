using System.Collections;
using System.Collections.Generic;

namespace GOALLogAnalyser.Analyzation.Queries
{
    /// <summary>
    /// Class containing a collection of query profiles.
    /// </summary>
    /// <seealso cref="QueryProfileCollection" />
    public class QueryProfileCollection : IEnumerable<QueryProfile>
    {
        private readonly List<QueryProfile> _profiles;

        /// <summary>
        /// Gets the count.
        /// </summary>
        /// <value>
        /// The count.
        /// </value>
        public int Count => _profiles.Count;

        /// <summary>
        /// Initializes a new instance of the <see cref="QueryProfileCollection"/> class.
        /// </summary>
        public QueryProfileCollection()
        {
            _profiles = new List<QueryProfile>();
        }

        /// <summary>
        /// Gets or sets the <see cref="QueryProfile"/> with the specified i.
        /// </summary>
        /// <value>
        /// The <see cref="QueryProfile"/>.
        /// </value>
        /// <param name="i">The i.</param>
        /// <returns></returns>
        public QueryProfile this[int i]
        {
            get => Get(i);
            set => Set(i, value);
        }

        /// <summary>
        /// Determines whether [contains] [the specified name].
        /// </summary>
        /// <param name="query">The query.</param>
        /// <returns>
        ///   <c>true</c> if [contains] [the specified name]; otherwise, <c>false</c>.
        /// </returns>
        public bool Contains(string query)
        {
            return IndexOf(query) != -1;
        }

        /// <summary>
        /// Indexes the of given query.
        /// </summary>
        /// <param name="query">The query name.</param>
        /// <returns>The index of the query. -1 if not found.</returns>
        public int IndexOf(string query)
        {
            for (int i = 0; i < _profiles.Count; ++i)
            {
                if (_profiles[i].Query == query)
                {
                    return i;
                }
            }

            return -1;
        }

        /// <summary>
        /// Adds the specified profile.
        /// </summary>
        /// <param name="profile">The profile.</param>
        public void Add(QueryProfile profile)
        {
            _profiles.Add(profile);
        }

        /// <summary>
        /// Removes the specified profile.
        /// </summary>
        /// <param name="profile">The profile.</param>
        public void Remove(QueryProfile profile)
        {
            _profiles.Remove(profile);
        }

        /// <summary>
        /// Gets the specified index.
        /// </summary>
        /// <param name="index">The index.</param>
        /// <returns></returns>
        public QueryProfile Get(int index)
        {
            if (index >= _profiles.Count)
                return null;
            return _profiles[index];
        }

        /// <summary>
        /// Sets the specified index.
        /// </summary>
        /// <param name="index">The index.</param>
        /// <param name="profile">The profile.</param>
        public void Set(int index, QueryProfile profile)
        {
            if (index >= _profiles.Count)
            {
                _profiles.Add(profile);
            }
            else
            {
                _profiles[index] = profile;
            }
        }

        /// <summary>
        /// Returns an enumerator that iterates through the collection.
        /// </summary>
        /// <returns>
        /// An enumerator that can be used to iterate through the collection.
        /// </returns>
        /// <exception cref="System.NotImplementedException"></exception>
        public IEnumerator<QueryProfile> GetEnumerator()
        {
            return _profiles.GetEnumerator();
        }

        /// <summary>
        /// Returns an enumerator that iterates through a collection.
        /// </summary>
        /// <returns>
        /// An <see cref="T:System.Collections.IEnumerator" /> object that can be used to iterate through the collection.
        /// </returns>
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
