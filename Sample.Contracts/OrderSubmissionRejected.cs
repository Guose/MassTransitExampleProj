using System;

namespace Sample.Contracts
{
    public interface OrderSubmissionRejected
    {
        Guid OrderId { get; }
        DateTime TimeStamp { get; }
        string CustomerNumber { get; }
        string Reason { get; }
    }
}
