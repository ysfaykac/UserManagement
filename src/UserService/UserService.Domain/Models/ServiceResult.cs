namespace UserService.Domain.Models;

public class ServiceResult<T>
{
    public ServiceResult()
    {
        StatusMessage = string.Empty;
        IsSucceeded = false;
    }
    private bool _disposedValue = false;
    public string StatusMessage { get; set; }
    public bool IsSucceeded { get; set; }
    public T Result { get; set; }
    protected virtual void Dispose(bool disposing)
    {
        if (this._disposedValue) return;
        if (disposing)
        {
        }
        this._disposedValue = true;
    }

    public void Dispose()
    {
        Dispose(true);
    }
}