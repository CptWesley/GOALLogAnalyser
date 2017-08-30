using System.Collections;
using System.Collections.Generic;

namespace GOALLogAnalyser.Analyzation.Modules
{
    /// <summary>
    /// Class holding a collection of module profiles.
    /// </summary>
    /// <seealso cref="ModuleProfile" />
    public class ModuleProfileCollection : IEnumerable<ModuleProfile>
    {
        private readonly List<ModuleProfile> _moduleProfiles;

        /// <summary>
        /// Gets the count.
        /// </summary>
        /// <value>
        /// The count.
        /// </value>
        public int Count { get => _moduleProfiles.Count; }

        /// <summary>
        /// Initializes a new instance of the <see cref="ModuleProfileCollection"/> class.
        /// </summary>
        public ModuleProfileCollection()
        {
            _moduleProfiles = new List<ModuleProfile>();
        }

        /// <summary>
        /// Gets or sets the <see cref="ModuleProfile"/> with the specified i.
        /// </summary>
        /// <value>
        /// The <see cref="ModuleProfile"/>.
        /// </value>
        /// <param name="i">The i.</param>
        /// <returns></returns>
        public ModuleProfile this[int i]
        {
            get => Get(i);
            set => Set(i, value);
        }

        /// <summary>
        /// Determines whether [contains] [the specified name].
        /// </summary>
        /// <param name="name">The name.</param>
        /// <returns>
        ///   <c>true</c> if [contains] [the specified name]; otherwise, <c>false</c>.
        /// </returns>
        public bool Contains(string name)
        {
            return IndexOf(name) != -1;
        }

        /// <summary>
        /// Indexes the of given module.
        /// </summary>
        /// <param name="name">The module name.</param>
        /// <returns>The index of the module. -1 if not found.</returns>
        public int IndexOf(string name)
        {
            for (int i = 0; i < _moduleProfiles.Count; ++i)
            {
                if (_moduleProfiles[i].Name == name)
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
        public void Add(ModuleProfile profile)
        {
            _moduleProfiles.Add(profile);
        }

        /// <summary>
        /// Removes the specified profile.
        /// </summary>
        /// <param name="profile">The profile.</param>
        public void Remove(ModuleProfile profile)
        {
            _moduleProfiles.Remove(profile);
        }

        /// <summary>
        /// Gets the specified index.
        /// </summary>
        /// <param name="index">The index.</param>
        /// <returns></returns>
        public ModuleProfile Get(int index)
        {
            if (index >= _moduleProfiles.Count)
                return null;
            return _moduleProfiles[index];
        }

        /// <summary>
        /// Sets the specified index.
        /// </summary>
        /// <param name="index">The index.</param>
        /// <param name="profile">The profile.</param>
        public void Set(int index, ModuleProfile profile)
        {
            if (index >= _moduleProfiles.Count)
            {
                _moduleProfiles.Add(profile);
            }
            else
            {
                _moduleProfiles[index] = profile;
            }
        }

        /// <summary>
        /// Returns an enumerator that iterates through the collection.
        /// </summary>
        /// <returns>
        /// An enumerator that can be used to iterate through the collection.
        /// </returns>
        public IEnumerator<ModuleProfile> GetEnumerator()
        {
            return _moduleProfiles.GetEnumerator();
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
