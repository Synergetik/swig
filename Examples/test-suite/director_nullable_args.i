%module(directors="1") director_nullable_args

#include <typemaps.i>
#include <std_string.i>

%typemap(cstype)       std::string const * "string?"
%typemap(csin)         std::string const * "new System.Runtime.InteropServices.HandleRef()"
%typemap(csdirectorin) std::string const * "($iminput != global::System.IntPtr.Zero) ? System.Runtime.InteropServices.Marshal.PtrToStringAnsi($iminput) : null"
%typemap(directorin)   std::string const * "$input = ($1 != nullptr) ? (void*)$1->c_str() : nullptr;"

%typemap(cstype)       int const * "int?"
%typemap(csin)         int const * "new System.Runtime.InteropServices.HandleRef()"
%typemap(csdirectorin) int const * "($iminput != global::System.IntPtr.Zero) ? System.Runtime.InteropServices.Marshal.ReadInt32($iminput) : null"

%feature("director") TestObjectDirected;


%inline %{
class TestObjectDirected
{
public:
    TestObjectDirected()          = default;
    virtual ~TestObjectDirected() = default;

    virtual void onIndex(int const* index)
    {
    }

    virtual void onMessage(std::string const* message)
    {
    }

    void nextMessage()
    {
        if (counter++ % 2 == 0)
        {
            onIndex(nullptr);
            onMessage(nullptr);
        }
        else
        {
            std::string msg("Hello " + std::to_string(counter));
            onIndex(&counter);
            onMessage(&msg);
        }
    }

private:
    int counter { 0 };
};
%}