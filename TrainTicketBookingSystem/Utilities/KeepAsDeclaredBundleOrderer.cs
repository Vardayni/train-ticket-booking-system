using System.Collections.Generic;
using System.Web.Optimization;

namespace TrainTicketBookingSystem.Utilities
{
    public class KeepAsDeclaredBundleOrderer : IBundleOrderer
    {
        public IEnumerable<BundleFile> OrderFiles(BundleContext context, IEnumerable<BundleFile> files)
        {
            return files;
        }
    }
}