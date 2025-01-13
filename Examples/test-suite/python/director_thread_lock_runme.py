from director_thread_lock import Foo
import time


class Derived(Foo):

    def __init__(self):
        Foo.__init__(self)

    def do_foo(self):
        self.val = self.val - 1
        time.sleep(0.1)


d = Derived()
d.run()
time.sleep(0.001)

for i in range(100):
    d.invoke_bar(i)

d.stop()
