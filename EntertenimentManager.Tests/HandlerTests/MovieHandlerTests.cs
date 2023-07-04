using EntertenimentManager.Domain.Commands;
using EntertenimentManager.Domain.Commands.Item;
using EntertenimentManager.Domain.Commands.Item.Movie;
using EntertenimentManager.Domain.Handlers;
using EntertenimentManager.Tests.Repositories;
using EntertenimentManager.Tests.Storages;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace EntertenimentManager.Tests.HandlerTests
{
    [TestClass]
    public class MovieHandlerTests
    {
        private readonly string _base64Image = "data:image/jpeg;base64,/9j/4AAQSkZJRgABAQAAAQABAAD//gA8Q1JFQVRPUjogZ2QtanBlZyB2MS4wICh1c2luZyBJSkcgSlBFRyB2NjIpLCBxdWFsaXR5ID0gMTAwCv/bAIQABQUFBQUFBQYGBQgIBwgICwoJCQoLEQwNDA0MERoQExAQExAaFxsWFRYbFykgHBwgKS8nJScvOTMzOUdER11dfQEFBQUFBQUFBgYFCAgHCAgLCgkJCgsRDA0MDQwRGhATEBATEBoXGxYVFhsXKSAcHCApLyclJy85MzM5R0RHXV19/8IAEQgCgAKAAwEhAAIRAQMRAf/EABwAAQACAwEBAQAAAAAAAAAAAAAHCAEFBgQCA//aAAgBAQAAAAC3YAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAHF+Bh7O3yAAAAAAAAAAACDoi6ZqPnUyfYgAAAAAAAAAAAFV5X63HF/Oui63oAAAAAAAAAAfEa9V0YqvK/XfPF/Oui63o0nFSV+wAAAAAAAAB+dUPnk7Td2VXlfrvni/nXRbb45Go3RfVuvSAAAAAAAAB+dUN5OWkq/aTVwbpLQbr54v589ft1N/V1Im/exvoLc+kAAAAAAAAPzqhvJy+saOq/azV0uXzxfyaiL+Ln7ekb8/bv0AAAAAAAACsP7znnGfnOcZ+c44v5ACN9dbQAAAAAAAAUos1ugAxxXyAHigG8wAAAAAAAAj6sNpNvkAxxXyAPFAdk5RAAAAAAAACPKx2k2+RjJ88X8sZGM+OAbJSmAAAAAAAABVjtJPAfGk4/Z/pjIxnkuKt6AAAAAAAABraYW7+2MseCCom170yBLuwArtcLogAAAAAAADVfgiHVTYDR1O54GysPvQR575zfrtwAAAAAAAgGINmzYneh8VI4sYZbyyv6h4oP+HllGygAAAAAAAphZ725GMkdVVyATlIjGRjOK+XfAAAAAAAFMLQesArxDIYCQJ3YyMZV7u+AAAAAAAKYWg9jGRjNX4xAHW2JAFe7vgAAAAAACmFoPYAVxh8Ad1PgAr3d8AAAAAAAUwtB7ACL6whhkmqTABXu74AAAAAAArVx+4J022Mn5U85kBs7NeljLzw/8vJ0tqwAAAAAAB88b5kU+6YQctU7WGD1WE6kxlwfimd+3afYAAAAAAADSU+tv9MZNDXyNPk7OZt6BX22HWgAAAAAAAAVSkWRQGs5flt5sAHMx7cMAAAAAAAADkamWZ6UAxxXyAauBrW92AAAAAAAAHJVLsv0zIxk+eL+WMjGWpgi1vdgAAAAAAACl9henABxXyxkYyamELwAAAAAAAACpvay0MZGM44r5ADh+MuMAAAAAAAAPHUCTpZzjX1k7iYtiOK+TzRvxE7bI4eO7ibMAAAAAAAAPHUGTZZ19WbA6eFtrY7ZuK+fwg/wTN1tZp62XDx5cPZgAAAAAAAAeOoHYx/YeUyp8v9a4r518S3EyjyrXccxcTZgAAAAAAAAHlhzs+6FV5X61xXzrout6OPj6YdgAAAAAAAAAELw/br3FV5X61xXzrout6fjUzvJ9AAAAAAAAACOqySbwlwPcr7xPWtDjTdVZl56jefm7LSuAAAAAAAAActUKyG0jqN7ge/4i7XD2Sl+/nqP+XaeeJ7c9wAAAAAAAABq6aTv0xHUa3C94DzVD6/tOQa6Nrmb8AAAAAAAAOWq3J3ehHsYXC2IPJUbspRclzjQ8parsAAAAAAAAOaiKKfVobMbPAzHsX3C2I8lQ+1lExxGn/GHtxmVpd6wAAAAAADnojiX9ZLkf3xbx85MMkfxVcPZvHUPuZOD8+Bjb2d1+PMcf+0sy51AAAAAAGhiWJfD2vE2wH51OslsjLDgYquD6Kgd7JoHAQdZTT64iDY6TbyxLnSAAAAAaWJYl1fd91sOej2xwRfxs5AODiT2yDJQGugXuZix49Hoon9+6/Hn+b3ctS10IAAAGviCI9N3Xd7IcH+U2B8VPshswPyrJ5bQ+oDgYNtv6AiPjen8R+HPc7v5amHbAAACpvzIW1AijuJLBGHFzngZ+a+bz28RY/wDQNdAfdzEDh4lmbxafTeQ8/Oeq44AAB46SWKAIMmTusjHzU2yG0yYg/Y998xtq7AZEfwhbf0DGeXr1YEeHT6byo1uruwAAEcQLMgBwMdfj2fed36yMuHnTLES6y1vI8UiTZzYa2BO9mA8/DcHxTv5HB+fAfjx88y6AAAgTnJBAD8eWhSeZMPmp9jtojmv17d5iOtBitswSuj+Ebbfuzwta5v6v0AHK6DSdfZIAABVjpukABx0e2dyIz4WduLhKVok6N+Wr+Nr4ZW7GA5Al0K5dH34A0/HeXxW9AAAU0mL2AB4692l24fNT5mhKymm4KypyEa8pNtaO9ji2/wC4eGqc97MA8vAY4m7oAACj9gsgBCcjSkYyRrXXs7IxL1815/GJtbDVlYEjad5dYyOAheewDEc/Ma3wAAAUVsaACN/LPwHzrK+SjGFleyeeIsV/n2ELJ7D9AIT1ErgDgvHF96/WAADx0lsSAD5r5YDsAGkrL83k/UiD8a9eqw3UAOdrTYn9ABxWrjm5PRgAA56pE9gAaSE7W/qAhPjboCLNbBfUT+AxVmVumADkdJwNrO4AABxdaJuAARVs5uAeKrN0NoR1zdfrSbUBEfFzIADmOd4iyUlgAAjqBJjAAfFdrJ9JkYyjHmLS5cPX7ppixkYzpquWI9AAOe5fj56lwAAEWV974AA5zmZmAIEsHunLQLP36AIl93XAAazRaKepqAABzkPAAAAAyAAAACVutAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAP/xAAbAQEAAgMBAQAAAAAAAAAAAAAAAQIDBAYFB//aAAgBAhAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAABZEAAAAAA1/Gy+rnAAAAAB4PFbfV+6AAAAATDwOL2ur95aoAAAAT5vpR4HF7XWe60fQqAAAAPN4X6Tm5Hl9roOrp8/6/wBIAAAAngvI6rDzban3NvmPR7moAAADzfnyUNqUw7b0gAAAHO8cDakOo6AAAABj+bYQ2pGT6BkAAAATyvKiNwOi6eAAAADxuFG1taw7D2AAAAByfMHvdvPIeGe91QAAAC0/PvKZ/pexOH55gb/dxAAAAW8zHw+F0nbjkedZewyejUAAAT8wxwd10I8XiRf6KAAAE/L8Y+j+oND58LfRgAAAn5fjH0v0BqfOhb6MAAAE/L8Y+k+kNL54LfRgAAAn5fjHee+PI4YW+jAAABbw8PIY3T9oOV5lbq8ns1AAAC1vnXmtn6ZmY/nuq3u8VAAAAcfzh0vZuV5w9zrYAAAAPF4Yejs6A6/2QAAACea5ANqR03RwAAAAr831g2pGb6BYAAAB4HFg2pDq/dAAAAeTwQNqQ7P1QAAAE/PfN6PX8Odp625z273kAAAAHk8N9Kzcdzm17vXYuA631oAAAAJ8X2o8HitrrPdnyfWgAAAATNXg8VtdX7y9AAAAAZcTwOL2ur95kxgAAABO/pU0/Ay+3t32tOAAAABl9PU0purXa2dLCAAAAN/bp5dBf0mvpAAAAGT07tLSG5u46aWIAAAE5L+hZTy6L+nkrj06UqAAAWyG/lNLSbm8a+mUpEgABlvU2tor5lfTyGprRM0pAABbLdSGbeGnG5JoYgUoABlvYpC3oWNeNkpoVLVTXEAF84ig38p5r0ZYNIXiEUoAMmYRQbW0xeXX1czU1heYitcYAy5QrEM29Glp03fQjQxpmwpXEAM9wVqt6FfOxUy+o0aryFaYgBnuCKG/jx68Zdi+iXkKxgAGe4KQbGTFpNjaw4SbhWMAA2LBFBbc1tSuf0NKpNpFYwADYsEUE58OvXNt6wm0isYACdiQVqLUxRfYxibhWMABOxIIoJx0ic8C8hWMABOxIFIFccWyibgiuGAE7EgVqGGMthNwRXDAC2cBEBjZAmQK4qgGQABNZABE0gAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAf/xAAaAQEAAgMBAAAAAAAAAAAAAAAAAQIDBAUG/9oACAEDEAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAIhYAAAAAC21u8zCAAAAADc7PY85xQAAAACJbnZ7HnOKiQAAAArsYJ3Oz2fOcRuaUgAAADP2vPV6fU7PE81f2/l+eAAAAV7m3y79CexPH1fRaPjpAAAAZ+7MJdiQ8jzwAAAG715Dsg85xAAAAE+gyB2Qr4fGAAAAjo9UOxI4XnZAAAATsdwY+tnHluUAAAAOl00aPGn1HYOL5qQAAAIjubbH5ujJ7rM0/FzIAAAVz27WRzuKPTd5TylNGQAABHpJS4WiOt65MR4IAAAI9JYed1hue4EeCAAACPSWHm8A2fdiPBAAABHo7jzmuNv3IjwQAAAR6O44WiOp7AR4IAAAI27da7mccek9Ar5vHypAAACI7u0xebqt7rYanipkAAAB0+kc3jvTd04/mQAAABtdsrh6m6PL8kAAAAb3XDsh5/ggAAAEehyh2Qx+HqAAAA3OyDsg81xgAAAGz3AdkHk+aAAAAjubPPybk9lzdXuani5AAAANns+ep1uj2eP5jJ7fzHMAAAAFdvVnc7PZ83xY6fMkAAAAK2bnZ7Pm+LNLAAAAApdu9jsec4qlwAAAAiIvk3t7k6tJmQAAABXPgtEJmufBYAAAARmrisK5bYZAAAAKZrMMkMyMFwAAAMTaK4bq5bGrOQAAAVx1bVzCmuxCmAvYAADHitDPmK4mW8MWGVMmQAAVx4oyGTZGFnROrjmaL5LAAY8VInITuDEzIacJrEzkyABXWqWuNq5hZ0V1BRaLZrADHrwWuM+ZSsssYtcUrMzmyADFri1xk2WG1seeNbGmKVL5coAwYQm8p24w3vjyW04RSBfJmAGDCDJJtVrVN7aoxBbLmAGtjBlGa0JTTCThC19gAa2MLXFs9ZtW2vUUqLZM4A1aBa4ZlrVpiClRbJnAGrQGULZLSwVDEE5NgAatAZJE5bTGCBGMJybAA1KgtcMt5rgClQm+xIBqVAyhbNOLGGOAm+xICNYIibhOVigKAm2eQCsAAlAABaQAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAB//xAA0EAABBAECAwcDBAICAwEAAAAEAgMFBgEABxAXIBESExQwQVAVMjYhMUBRFjU0YCMkkCL/2gAIAQEAAQgA/wDvJ+mO3UjuHTYt9TBPNWia5q0TXNaia5rUTQe5NKPeSy0lSVpSpP8A0jea2FxQgcKDWdt5eyA4P1yXkdcl5HWdrjcZzjXK83UzQZOJDcMTstbC33iK6X/0jfL8mjtUT8QgNe+s/tpf3q4TGMZiZTGdm/zcb/oTjjbLa3HDt3aSC/lnEBbICzNqXF9G+X5NHaon4hAcM/tpf3q4S/8AqZPWzf5uN0zlkha4Ph+UG3gpJL+GsjkDlstPj/MvPNDtOPPbkbkO2R5yLi67tMfLAJMPOAsG388y4ijXkC4gduOO+X5NH6on4jAcM/tpf3q4S/8AqpPWzeey7i9FwuEdT41RJJJE9fJ5bzsjtqWMGp4Tb/cAuoF+SMDMFPGYKE+XeeaHacee3I3IdsjzkXF7ebeYbwxMzOp+Aj7HHuBGngT+30+0tFHvcfb4/K82XdiswOVsDTe7NumlKbHBpdxsDmH81+MVDQsbHL1n9tL+9XAxjzQhQ+jKjZYheXkQu6NxglJbcre8NcmMoYkLbdImqxWDXSCJ6+TynXYCBDgA0sMauVNTIpXIx23+4BdQLwEaGYKeKwWJ8q880O0489uRuQ7ZHlxcXt3t53PAmZrh/Wp+Aj7JHuBGzdZnK3KLBzXdpZM7CH5iFqNegUp8j/XHP7aX96uiSgomWTnBk1tuSxhb0UHCzMmc1HYgYAOADwwxxuVNTI4ckY7b/cAuoF+SMDMFPFYLF+U3guhZUk9XA9vNvcIwxNTPD39D31n9tL+9XqXKnJkUuSEftVcy4OZHhiflNwBX4u9TOX4KaAn41g4H086X96vUPPFjBXSyoRt2dugHlvlL9QxLiD3kQ8xOUGcebdhJuPsEe0cD6WdL+9XpnnixgrpRc3NydukmWGNudvGKmNg435W/UEK4BZcbh5icoM4807CTYE/HtHA+/T79Gf20v71cfbp9uB54saK6UVNzcnbpJphjbrbseqjpON+X3tlgDJ4MEfaqDfioBRT/AKC3G205W49aK2xnKXMWGCecVhDbrbqcKb17dPtwusU7LQTyGdoJUCNtjTJny8zJsQ0VISL8CCTc7c3glCUoSlCeqRkwYkRws+xbvGPqWxBSEvKSrmXDuApxgK8OCw24x42UtycbKASwySQ+u2RrkBYFqGqc2ixV6Lk8fJkzkKG5loj/ACet6/yet63jtYD0CJGR2zcPhoKRl19U/PAVyNePNslmk7OcokzqiZc2FLQSHATwk+Fghjq3GjcFQ7ZiNlrQGCNLxUh/k9b1/k9b0zYIIhaWmPj93r0bHPIgIyKoFpmxUHD8qbjrlTcNY2ouGc4xmuQ6YGEjo3HSpSUYUpV6tLlnmHFo6PfXZxrs2/AyTRSGXmyGm3mumQDRIAlhrXt5Y0rVhPL2y65e2XRlJsQLDhK9o72d9Qarsl8dun+t6mu2OSlEeAlPT79G500qIrLzbX6eltzKZLiXQ3OHt0+3DOMZ7cZpeMYvUBjHx26X51NaA/4AXpbzGqXKxIPpdutuCssz62ePt0+3GmfncD8ful+dTWgP+AF6W7mVZtnp0bKsWeO7PSpn53A/H7pfnU1oD/gBelvKHlubjC/T26Hy9YkuenTPzuB+P3S/OprUf+oAPpbrQypKt+ba6ffXZ0baxmWI8o9fpUz86gfj956iXg/FjEhd1J2IAZCXznmdc55nWN55ftx2wsqzNxQEkzr36HmmiGnWXbdXH6zMvhr9CGiiJqQYDYEFYBFHFY4e3EslsMUkp1e58h31ZRzPk9cz5LR240wUO4y1tBUTJGaYnH/jlJStKkLK27pRjqnXeWNG1yxo2t2KJDwkQBJQ+z0vgmINi19Vsq4dqjVCvTEPIQRzoR/UGEVIEtDC1etMV4TOM8fbjuFJYDg8jp2jpUZYW5Y6X5Y0bXLGjaY23pI7iXEtNNMNoaZ+SskO1PwUnGOU2WdqtrZyV24zjrnq7FWMTI0hYtrp2JU46A424ytTbvBOFKzjCYajTMplK3oSvR0Cz3Beu8SuZadWy1R4HFcrEWBn5feevNxk+zKMbaWFU5X0NPegdERconsOf22pr2cq1ii1hpzOdBxMZH/8T0LbM5hIZ99G1NfRPWpl8j5e4XGNp8aoklWbFuJYsqXWKyDV41IYvo50v71elMRIk0C4GS+xOUebZeYo15AuIGM4+VuFwjqfGqKJUqxbiWLKl1irx9WASKNr36ffoz+2l/erj7dPtxmIcKbDcFKfYm6PNtPM0a8g3EDGcfKbkSJU1d5NtdWrAFXjkDDennS/vVx9un26JiHCmw1ilVgsqtXQDw/lN3asXEz700zt7f25ptqLk/Tzpf3q9S3W5qDaUKLtrWDbNZRzXflJAAOUDICMvlEPpZ6SRtv9wG5tDUXJ8ZSTChwXzjZ7cefk5PJIVd3fbzhDE9HSsbLM4eA4fprOl/eroKMEBay8VNbkDM4U1Eg3adFkMFPRUqHMBtFi8bdbmoNpQo1Op0reZVxWYeIAgo9iPA+VkI8OUDICNvdEPpZ6SRtv9wETSGoyU1JygUMC+cdZ7NLXiWZYYoe2MfAx6npay7KRZ2VvwclULpUX8vqi91rRH91BUNJtzEUDIt/rrOl/ergS+kYd99chuLNld9IsfWrhbn8OM1vZEMfKH5+4bZw07EoZjgDpmlzL7L0VKhzAbZQmrdb2oNpQwtOp8reZValQ8OBBR44AHy54AcoG+Gbe6IfSz0kjV7d1hkBLM3ZbPLXiUYYa2626Hqo6TjuGcduOzO9YootmC8CifiEBwzpf3q4S/wDqpPW0YoxdzFbIwlKcYxjhfqCFcA8uNgnzNKmH2HpLcoXISsR9Pp0reZVeVQ8PHwUeOBH/ADJoIkkK8IYfsbAPv5cEqu3leqa8vi9G+X5NHaon4hAcM6X96uEv/qZPWzX5uN02ijQFtSnJ42xcE2/hREXFR8KE0DH/ADe6W4X0JhcLFUnciWrJ/YZGyQUuEOcDx3y/Jo7VE/EIDhnS/vVwl/8AVSetm/zcboJJHDYeJJv+5Z1iLyJF7U7irN8OAmPm9w70xUI7w2K1Xz7hLuukW3b4Q4ND0PR7vI0eSWw/GyQUuCOcDw3wrxL6I6dYqO56oCMajTuc8VrnPFazuhH5znOuZ4GpzcT6gA+IHsjXinJIqdd4FljAjPlE7hbhlWwrIQVerKGUYKPnoZ2GKS8xtnf0WcPEcf8AM261gVGJdNJbbm77YXHHYeIDgwGQRNXaktzjazgaVd5SjyLjD0XKAzII54GnWm3m3GnZHZ2mnvqea5IVTXJCqa5IVTXJCqaC2ZpwryXHRhhwx2hhtFligDPFFbhbgl24vyINerPk+6UdooVg1hxh8hiRq0sw+PRboJcIpL2Pl5qZAgI0mRPsE5M3+wpXqrVoWtR6WG+N1pDU62o4Gl3SUo0mtl6LlAZkEc8D0DDBY8V8su/7gGXAvAYlGoqY1LUpKTMZ46cks8JONYlBVsPRcnL0qebKYrVjj7RFMSIXys1daxAYVg++XYu7SjbI1HqKK+JgknputJanmlGBU26StGk1tORMtHzYA54HUccJGiPmGX+/mXAzAYVHoqItLclJ8JqM8PKiWOE9Cty436U62SFHmsrzCX6qT/dSHjOM/IzVwrVf72JGb30YR3kQkperpZ3MsLits7NJd1b8tGnU2wJa1ESTMxGhnM9V0pLM+2owOm3GVosqtp2JlgJsAc8DoOOEjRCDDL9fjbkdgUWjUZMUluSkuOcYzjOMy0ZkNzxG9PvtjMuvONtkWGZ7NG0qUH7yh4233KrOJaZhd9PsRNwt4q0/3Eg/FzFsrsAlX1Ga30Ba76IaW3CuVic8FcVttZ5TKVvRO08GJ3VngxkdGN+EFrdGv4kohMkztZPeE+RCvdd0pbNgZUWLULfLUWWcQuHmI+djx5CP4SEgHFhkGm3y+nXQ1IotHo6IdDcjI9LrSHm1NrkAVgv5Rm5yXcaaAbpMX4Azh7mnmGSEZQ8dTYkrtyydS5UbvKYirxcKy5hhmE30az3ETcLdKxP4TgD4WXtFfgU5zJTe+Uax3m4aX3HudhVlnIFHsUlnC3B9vIsAV8k+hDYLt8QnpcbQ6242uejiqfZ3EMxEmxMRohzHXcqWPYGVFDVG3S9Dl3ELh5mOngGJCPkJAKLDINNvV6kLtIJGGo9HRDpRIyPXYpJrC3nFAiF2qwNMIzVmGWW2wyIk4btyr9e3PG6M+HNKXpNdjzgxn2iazJDZypmJvtyra0ttQu+gq+43NQ9wrU9hP0/+fLWOCg28rkprfGIH76IiY3Muc8rLWAabY5ZfiuR22scx2KPBio6NRhAmrUR5auy69bSi+LYiX+rdGv8A1KHTIs7Wz2WSH4Z70LnTGLExkkaqWyYocu5jN3vcldjkDsUikIhUIPkOuTLwGKteLnJ5Q20Ajaev+XDfmn+BAAhXb4pFbRn9RyIs4btyu/M9jsc9qvu+LEi8Hh2CE5Q8VVgHu3LBNbkxc99qI3DuddylpELvmA73W5qHtddnk4zHfyZSWjoURRkjN73wovfRETO6VynMqaSFULLML8VyN21BZ7FSAEPFxicYD6NxCPBr2W9bOC9jU6XnpW2h1C212GMJqFmcQxDyjEzGiHs9bzzQzTjz1wnGrLNeKHXpNVYnmnywzBpAVgoXrsUm3hbziwBC7VYGWMCCsBCjisdO7gLeYUEtFFEVIRxqEPx5Y/blzi+MOSnKXyqqC725YIr0qGrC2ofcm5wCkt6hd8ox/uNzEPOxE+LkqL/jbxWRcrYvpLMLtyMtgciSAhYmLxjAfXueR2NxI2tqhfBq2HevdCv/AFOGxIM7Wz3gkkQz3VnOEpyrN8uapl5UaBS6n5BCJE64VVMwzksWl25+tmZDMadbfbbda6ZMvAYq14uknnCWgEbUV7Iwb80/1bii+aqMprbF/unybHB8AQjty4/AZ/XLD4BY/b4nF8QYpPdfMqojiVKG2wsTtctY4738WRMbjgDTXIBl2wWphwj0dyH/ABJxhnVDOisVyJDY6loS4hSF2GMJqFmWhiFlGJqLDPZ6dwLr4+XYaM2722KOF+vHqSpOcpVq6VPBiXJIChXPMS6iLkMZwrGM46LFJt4W+6uODKtVgaYwMOyGOwOx0e3C1HRLcLKCl0F/wbKInpfjgyO3vvwCv1ywQEUN+rvCxMZElcut1qUxNQERI/xd15HMfSZTGNsRO8XJF54+/VZqQmdLycwbRbGDnKkDWa3QK8Npjt3pNnsTIR26FXN7qXgzg5BhJAfHc6v/AFWFwcztbPeAU/DvdF/uvk0uw8btjt4qwvpl5RKUoSlKbFE97CjWOF1qfh5clANvrr3PBhpPjKF+UEcVi5yfYluPRtRAeVBfmX+JJQwbLj5MjuZVQe9huQ3gOc7yY8u3W6cXlvIdKssgrC1V2hfSTmDyuHvw/vg42l1taFkMKHfdZzq3Md5gV7Wy0jkyorGV/E33OymNggNbcDeFBOPcPfj/AFw9+l4cchOUPG0euG9ucWAAGMk3xAtr4QuKhXSSuK0JcQpCrJFkVGzOIYhZRmZiwz2uF6uSYNhQALDzeDGHyq5IRMpCx5UTrOMZxnU7E5Be8VrWcYynOFWDACJk/EfQLriQQ3EyXCxyjaVPuqjASrXYWR8CjMhjsDMcdwoUubrzrYsSKIZIjjGg0Ouh9mVjiCiowgf0Z5nuvNO41YmfFiStbDndhk+Bn+JvkZl2yRouKixgatxKPVuVixBx+W2aBVVWKUyUVjGMYxjHRudX/qsLg9na6eyOY/DvauVtYrYfcbrsBIXCWcW7cKTHvwaVgV662KrNkNRXN69a5vXrT+7F1JaU09/n9j07erE8041nbmvjTUkU+Zb6qVVz0vj0e5InmMBlyheAxHFYuknnGGgEbU1/ygD8w/07lVP6UdmVDotk+qB+QJ129Pb0TjffCyvgc34wZTetnDMjXVhr+LuwV5m9S+NR7PlgAmPUPOHjQ3zCXFSVysCUtwcOLAxowA3SpKVpUlVniiKnZnEsGXqPFrYktoAGXus2vOYeICgwGQg7yZ5Gryy9bLQo8nOSZJX0CC19AgtfQILVlBhWlMDMWqNFVX5Pw9piO5KybGpCPElA3wy56DkqdLt5baubU0BkkmLBJtViYYwMO0IOwMx0yUcNKgkAlSAUlTLBlvUPKDzMewYx6JrfiCEJ4Z/XGcao730+9QSv4tnczI3qXzrGOzGPU3Emn3z8RaNsa0xGxSZZzr3NgPq0J51kEE6TeSMHt5YMQ0z5Z/W7JnhxsYHjY6P8vXZE7PAh5IzDryiH1kvuvOSLOCI85jW3D/gWsJHDc6wpNMaiGDI86P8AA81tTXvJx7sw/wAPbp3BrbM5DOkJoc0/HyzQXpZxhWM404nKHFp4eL5C0Nv/AMWKV9Rt4rvT79X66/vV/r3nRcSg+1tp8q/mDL68pwpKk5j4SJilvrA3IrOYWW88NRLHieiEoe3SM8efZGxt/H/TKbAMcTG/FFJRwzjGcZxqFVmNuQeM26wIrsO8Tij15yzTuFkyENFyrbTZzTbbLbbTfXunae4jECJt5Xu6nMyTr++Pvw/vj76PR3DSccLEjuS5OgH/ADQARH8ORe8tHnP4pDfiWaO1/XD34/1w9+nOMKxnGbbBuV6Ww6NSrMiyw6HV+hYYQewRJQD0HJGU+xZy/KO/5Jb3fBYaSwyy0jiQjLT7yOFjxkC1HLxb5520TeMDVGut1uGYE9G0T7FbiHzVwcYXapxWX2mm2G22mvSmU9093hakd2TTnVPe8zVa87/Dtz3gVewOa27b79jTn1rBDMzsa8IuszZdPn8LeYfaJZafZ9Ddas99CJ4Xa2PxIXaHxnomW/DlDU8NwmPCsOV62rrOCilzhPoKUlCVKVeLI7aJruDVWBRAxiGla7ent6Z5PYU3nhbk/wDtCL1tq949Grq/4e4ruWaRY1a2zRjMyav19w6930YmBtq7T3k5gC/QkAmZIEsJ9tcpQbUlaYaWEnYsKSD42VvuSilaedbYacedkyzLdYEoHhItuFigY9v0N0LTkAPEKJt7XvHezLkepYE//sVXC3p/UFWto3fEosZj+Huu54dEmsa2wR/7ksv13G23m3GnJ6LKq02lTFVsDFkh2DU+huVV/rEZ9RG2duGIuRzBGcbYjsfEc1uDP91KYgfa2r+VGzOFa9un24T0yLARZR5AbEhcJ9WXRRWAh2BmPUsCf/ENnhb0/wDgCVrZZzv03KP4e8a+5SCsapNhjYHMhk3mJXNcxK5rmJXNcxK5rmJXNcxK5rmJXNcxK5rmJXNcxK5rmJXNcxK5rmJXNcxK5rmJXNWizVmfjVsYpNoVWJTxHealU1zUqmualU1zUqmualU1zUqmualU1zUqmualT1PvRapggmGr280HiHCRNc5qVrnPStWfdavHsY8gC8IXMNPzDW6FRabbab5qVTXNSqa5qVTXNSqa5qVTXNSqa5qVTXNSqavlwxZjGWhKjYK5AAq8bmJXNcxK5rmJXNcxK5rmJXNcxK5rmJXNcxK5rmJXNcxK5rmJXNcxK5rmJXNSl4gTGW0Nf5NE6sEuDICstsbGL71Yk0fw7VWxLXDPxhPIRjXIRjXIRjXIRjXIRjXIRjXIRjXIRjXIRjXIRjXIRjXIRjXIRjXIRjXIRjXIRjXIRjXIRjXIRjXIRjXIRjXIRjXIRjXIRjXIRjXIRjXIRjXIRjXIRjXIRjXIRjXIRjXIRjXIRjXIRjXIRjXIRjXIRjXIRjXIRjXIRjXIRjXIRjXIRjXIRjXIRjXIRjXIRjXIRjXIRjXIRjXIRjXIRjXIRjXIRjXIRjXIRjXIRjXIRjXIRjVNqIlNi3AR/wD7k//EAEwQAAIBAgEFCgoIBQMDBAMAAAECAwAEERIhMUFRBRAgIlJhgZOU0gYTMDJCUHGRssEUI1NUYpKhsTNAQ3LRVYLhRGBzJIOQwiWio//aAAgBAQAJPwD/AOeXd2Hxqkhljxlw6UBrdsdTL3a3bHUy92t2x1MvdrdsdTL3a3ehDkgDxitEPe4pgVIBBGsHQR/2TM0Ul4jPO6nA+KGYJ7Gq5itbdyRG0gLF8NYArdy36tq3ct+rat1oOrat1YerariO4jjGMgUFSBtqYyRrCZbQscSoXSn/AGT9wX42r7sN/ad7XaTfAa+6z/D/ANhSKiKCWZiAABnJJNXs1wQcC8ERZK3QSVlGLxHFZF9qtwfuC/G1fdhv7TvfdZvhNfdZ/h4N/HArY5CnO7kclRiTV5PCCcBJLCQlTJLDIAUkQhlYHWCPXUixxopZ3YgKABiSScwAqRk3KRuMwzG4buVemxMgxii8VlvhtfEjCpTFPE2XBOnmSr8wdYorDuhCo+kW+PRlptXgfcF+Nq+7D99/ad77rN8Jr7tP8PAYPcPiLe3BwaRu6NZpjLcTH2RwxjUNiir0XMyjFosjJJ5lzmsuTcuR/rIj50LH0k7tTrNBMoeORCCpB1gj1xIscUalndiAqgDEkk6hUjJuUjYM2g3J7lQcbM1tbONGx33o8QcTHIPOjblLUrRTRMXt518yVf8AnWtFYL+FB9JgJ97rtSnO6N2umOBuIOZnqcWMLaI7Ycfpc56sZzl6Z7pimIOvF85qQO9vCqMw1nSd/ad5sPHROmOzKGFWruEJwlt2LZh7M9Xpu4V0w3YLn8xwagdzbk/aHKiPsepUmlmU/RYEbPMe6NZpjLcSn2Rwxj9lFDKkYAzSkZ3bejAuwMZIxolHerLk3LkfCSLXCx9JPmKnSWCZA6SKcQQfW0ixxRqWd2ICqAMSSToAqRk3LRsGbQbk9yoONma2tnHSHccCPEEExyDz421MtRSsZcVhkhBImRvZ+q1KbOE5xEM8p+S1udGJBpmkAeT8x4O08CxjduXhkuPYwqczp9jJmfoOYGoJvGIckiTELEvToFKGkYAzSkZ3PAjAuwCZIxolAHxUXk3LkfCSPXA3LT5ip0lglUPG6nEEHOCPWspS0tsn6SV0yyEY4exag42Z7a2b43+Q8rtPlEAuwCZIxolHeqUnc+8l8XkNphlbQV9pzH1qmON2LhMdDK+DipQyMAGXWjcg+V2nykoSJBnJ/YbSdVRENc7piZV5C5fjD7h61wi3SgU/R5tR/A9QujIwS7tXzBwPnsapQ8bDjL6SNyW5x5TafJyiOJBnJ/YbSdVQyFDJkW1sgxJJ+ZoLJurMnGbVCp9BfmfWwWHdOFD4ifl/geoXRkfIurV8wcD57GqbLjfMV1o3IbnHk9p8lKEiQZyahcoZAltbJnJJ+ZpUl3WlQYnSIAfQT5n1xBH9Is4sLicDjEvoj6KJD37iUJyYwMF6TRo0aNGjRo1IqKBiSxA/et3LIHZ45TW69qc/2q1IrrtUg/t5EnxkDidVHp5AIw9xqCMtdxmGCZhnjkPe9cH6u2heT2ldAFMWN1cPcXTfh85qUKqgKoGbADMAN8cC5SGFBnZv2G0mrcQRZx9IlGMh51XQK3QnuGPLcsOgb91LC+OlGKnpwqIXMX2i8WQfI1cCVNe1TsPkMY0dxcW7D0T/AMGiMuaEeNA1SLmb1putZxSD0HmVT0gmt3rDtCVu9YdoSt0IZzdz5U3iZA+CRZ8+FLxpnEER/Cmc8N+KuZEHnSOdCrjUmCAnxMI8yJdg+Z4cuSw85dKsNhriuuAljOlG4a8e1kGP9j5jV9FBGrpPAZXCDFszjPW71h2hK3esO0JW7NnI50Ks6E9GB9YXDQu0Ye7lQ8bB9CA1ZAQyZ0eaQIX5wDVrb9ctWsHXLVvbgbTMtMGaGMB2Gguc7EdPCICgEknYKc/QoMUtk5uV0+RJMRIWZOUhpw8bqGVhrBGII4XmzwumOzKGAPRUUDqCcGEoz1bw9atW8PWrVoGRM58W4ZgBzCrhpoZlP0R3OLIy58jHYfV/Ki+AUuCi3jAA/t8k+TNeuIFI0hTnbybYvaPgv9j5x5IV/qcfxer9sXwCvsI/ho0aNGjRo0aNHNFamXpkYj9l8meLPbuvSvG8n/qcfxer9sXwCvsI/h3xwRvaBZw4eT/H8J8n/qcfxer9sXwCvsI/h8kOLNZ+L6Y3Pe8mM0MEjnp4vz8n/qcfxer9sXwCvsI/h8kmMthJ432xtmbya4G5cIn9ieT/ANTj+L1fA0kEkapd5IxMbJmDnmIq3gukiULG8mIcKNWatyLT3vW5Fp73rce0I5melyVuIg+TjiVOgr0HhIHjkUo6nOCCMCDQJgJL28nLjPkRnc8dtSqNLGlyYokCqOYcI8SGNpG9ijE4VuZAE1YsxNbm23vatzbb3tVvBAXBBdcSQDsqFksrMlo3YfxZtQX1eoKkYEHAgg6iK8H7YOSSfFgxj3LW4EX5371bgRfnfvVYC3WOcx3AUk4iTzTnpuPaS+MQfgl3jRo0d7BJ0xa3mwzo3+DUBilQ9DjlKdYPDiMkznBVFYPdygGaX/6jmHDfCS6cJ/tGdqshPbxmOGBSSBl6WOatwIvzv3q3Ai/O/erwet2YanxcdIJqNI40GCooCqBsAGYD1n/1EDKp2OM6noNAxoJHtbtTqBOSfyngDgW2VhjkSrmkjJ1qaX6fajE4x5pVHOtRsjqcGVgQQdhB31JJOYCovotvy5NJ9i1FjIQA8zZ3fyByorb6mPDW2s9JpcJvF+Nn/wDJJnb1wVEe6KlnQHOJU0npok3NjhA55S4cVvI7nQXH/kjDGtxwh/BK6/OtzsrAnzpHqxhhIGlEAPv8j/GkPioeZmBOPRhTKYbAC5ZSc7sp4vrhg9w4It7cEBpG7o1miZrmY+yOCMfsorjOxDTynTI/k9p8kvEJxVhpVhmDCpWimibLgnXzZF6dusUVh3QhUC4t8ejLTavrZg9w4It7YHBpGH7LtNEzXMx9kcEY/ZRShpmAM85GDSN/jYPKbT5NMQcSjjSjbRUrRTRNlQzr5si/86xWTDfwqBcW+PRlptX1q/FhnFrAupFTNSgzOAZ5z50jf4G8aNGjRo0aNGjvbT5RMRhijjzkbaKl48N/9Gkw0OpfxTetYybK/YPljRHNhnU+3TThL9FAifVOO/wRwRv7T5Qh7916IwfSNKxs7ScT3Mx1uOME9pPrWBZreZCro2sGmeTc55Mbe4GmNhnCvsapAu6CjCKQ5hOO/wACcRQxDEk6+Yc5q9ms7aM4QRRnUNb7SatiraPpMI+NavYriPbGwOHt2cHaeBcRwxjSzsF/eoTM/wBtIMEHsGurx50LfWQscEK7ABoqTKRhnGtTyTwGD37r0RA6GNO62yuHu7tv2G1zUAit4lwAGknlHaT62t1mt5kKvG2sGmeTc55Mbe4GmNuS9Shd0FGEchzCcd/enEcMSkknXsA2k1FIYfGZFpaJnOJ1naxq2iu90LmPCUOA6RK3oLVz9ClOfxEmLxH5irK5iCaLq1LMnSy6KeK9jH2qhX96UhRbmEPkHUTq39p3hisUbOcNijEgUsdqhJwKgM+HtNWV3chtM82Kxj/c2Aq8+ktrtoCVj6XzMatYLK8tUItXRQqkch6geN43yLi3f0gNfdNSBkYcYa0bWp3mD37j2iIH0jTutsrhru7bP0Da9W4igiGAA0k8o7SfXFuk1vMhV420H/kaqaSTc55Mbe4GmNuQ+xqtppLmIYCaIAiXnbYagfxJkCWlmnGznbtalWXdWVM7aRAOSnPtO+MRVtFF4yyVnyEC5TZbDE192FDe2ne+6zfCat45kFvOwV1DDKC6c9DAagN8JDunCh8RPyhyHqBo5I2yLi2fMHA1901ayi6cYYyAZKU7rbK2Vd3bZ+gbXq3EUEQzAaTtY7SfXVuk9vIuDxyAMp6DW6d1bRk5480g6DUTz3hBBuJsC4x5Gzg/cF+Nq+7CjvbTvfdZvgNfdZ/h4NuUuEGCXMRCyAbOcVuvdzRY/wAMBUq1SC3j81FG3WdpPryf/wDIzL9dKpzwI37OanmvNz5mHj4ncuy/jTHXVws1vMoZHXgfcF+Nq+7Df2ne+6TfCa+6z/twJlihiQu8jHBVUDEk1cS225sLnJKMUec8pubYKuC1xotLhznf8DH14yvuncKRbx8gctqlcxZfjLu4bSSdQ2sagSG6gjCiIZhKq6v7qjkexeTC6tjpQj002NVws1vMgZHXfjLxwoYLjD0ATirVYNcRQ4+JdGCsFOo1uNdfnWtxrr861uXP+Za3LuPzLVk8RmQo8jsDxTmIFRlbaKJoIW5bvpw9g350igiUvI7HBVUZyTReLcqN+Imgzty3+QqINI68SI+iDt56LCBmxjcaUYZwDUgG6tunXpyx66IaU4rbwY55X/wNdSGS4nfKlkI4kUY/YCkwRPObWzHSx3lVN0EXONUwGo84qN3snkwubVsxVhrXY1XCzW8qgq4/Y843o1eN1KsjAFSCMCCDmINQT2hYklIZOJ0BscKvL/rFq8v+sWry/wCsWry/6xaW6uQPQklwXpycKgSKGJQqRoAqqBqAG9OkMESlnkchVAGsk1lx7lxuPFxjzpm5T/IVEfpHoRnN4v28+8gZHGBHzB2ipnjkicSW845qKx30IC3UGw8tdqn1xMI4IVx52OpRzmo2ZpHKWtuDmij2d40A9w+DTy4ec3+BQ31VN0EGcaBMBqPPsNRyPZtJk3Vo2YgjWux6uFmt5VxVx5GdIYIlLPI5wAAoPHuZG/1UXpTNy3qLG8IBhhOiLnO1qX6wDF1HpDfGB0o+tG2ijkTQtx09CaM6VPMafM+aSM52icaUPrbdeBJAD9UhMkn5VpZFsInybWDW7H0mHKpQd0JlGWfs1OfIHz4Sqm6CL7BMBqPPsNK72ZkwurNs3Sux6uBNbzLirD3EHYRw50ht4ULSSNoArLj3MjfCKIaZm5T/ACFRBr04GOI5xCO/vrxCcXUajtG+ALhATG//ANTSsbdmCXltylGv+4aq3XjExw+pmxifoDYA+st1oIpAP4QOXJ+VcTW5JkOqW6OC9CrW6E5V9FvagoD0LnNRJZxnXMeN+UU+VLbSRywyYYBxpBrzJ4wxHJbWp5weGBHugi9EoHotz7DUbtatIFu7RviXY9TiW3mXFWGo7DzjgzrDbwqXkdtAAoPHubHJhBBrlblvUYN6c8cZ0Qjv8AYgih9S5/Kdm8cEjQs3QMazNPISTyEH+BTJcJzHJat0bmJRognxePoVq3I9s1r3HrdeEynRDJjHJ7m9WbrQQuB/Dyg0h9ijE1uTJMdUtychOhRW6cqJIcBb2oMYJOri5zUC2cZzl7g4N0KMTU8t4+tf4cf6Z6soYE2RoFx3kxuLHEttaE6R0U/Flxlt8eUPOXyChN0UXMdAlA0K1RubYyZF3aNm6Rsep1lglGY61PJbYRv3Cw28Kl3dtQFK8e5ySYQQDzpG5b1GGvmGMaHRCO/wlxVhgRRxQ50am4z4PJ/aNApePPxY+ZB3t6JJE5LAEe40Ht3/AAZx0g1kXCfhODe41ulOqxkA284y1HQ2JFbkMu2a17jVuvC8h/ouciT8rept1reBtUZcGQ+xBia3MkuW1SznxaVujJFG+iC1XI/UZzUHiFOl5yVP5c5q4e5MUbOVHETij30MyytL1aluCoZHUqwOcEEYEGiV8RMJ7V9sZOK0eJNGGw5J0FT5ALHuii5m1SgaFao3NuXybyzfN0jY1XCywSjSNKnktsIq4SG3hUs8jHRSyJuekgFtbDOzsfSfa1Rhr5hiiaoR3j5BsIbZDiecZzhXn3UwyjyEGk9AqUoqIFVW0YCoCy8pON+3A/qQo3SM1F4XeJGzcZcSMdBoCZfwHBvca3SmMa6YLkeMXoDZxW5LxHXNbHKHSprdiCVyP4RYJJ+RsD6g3Ut7fYrPxz7FGJrc2W7fVJKfEp3jW6D28b6IbQFP1GLVAYlfOZLgkH3HFqu3uG5CfVpVlDDzqoDdJoVpMDJ1nFoZobRve5AoUKFChSYz2OJO1om0jop+JNjLb48saVocEb6rHuhGvFbVIB6DVE5hL5N3ZvmDYfs1RvFYJJhb2ozl25b7WqMPfuOKmkQjvHyHnnip7TTcZyHl9moGo/rLjGODmjU5z0nfgUnboPvFTkbFf/IqAleUvGFDSsiH/aQa9EFD0HehV1/EAf3pmhbm4y0BKAczIcGrdGWSNM3iLoGQdGVnFblPA2uaA5adKnAit1oJm+zysmTpU4H+avI7a3UgF3Os6gBnJNWMt6+qR8YY+9V8bSJtEdoCh/MMWq3dA+dpbhsMeg4tV287ciPiJVjFFtZV4x9rHPvDfOeaeNegcavSeGMdGJNGjRo0aUMjqQynOCCMCCKYp4qQT2r7UJxWvNmQEjksMxHQfISBI0Us7NmAAzkmrUBFAijIXCSbnarIOYiUkRxxkx0suxhUwkhlUMjjyD4Q26nPzjSaxy7qbjHkINPQBShIoYwiKNQXNRo0aNGo1DR3QDMBy1NSgPDMMx2OKhOHKXOP04ECSD8QBqR4W2ectDxmToeM4NW6Mk8aaYbzGX9TnFbmSWza5YT41Kvo7mHHAldIOwg4Efy7n6LufgmA0NMwxY9GiriUu6hzAmCgY6iasYozywMW6WONGjRo0aNCtbSOegAUM89zI/QvF4aY3Fjix54jpHRT8SbGWDHljSvDIAAJJOwazTn6DG3HYZvHMD8IqMfSXGMSH+kDr/uNJhexDrVGo84rKNjI+EiHTE3KHzpw8bqGVhnBBGII5jwjx24qdNNnfB5fkDSfWXIMcHNGDnPSRwxnjEco/wBjA1/UhV+lG/53oBjyhmPvFTexXqA4bRnHAgSQfiAp2jfA4KeMtOVtrx/o1wmxjmRug/y3mW8MkrexBjXHMtw1zNjrwOWfJaIbZfexJrdCB50hxkjEgywzZyCOGoZWBVlOsHMQRTFRDIJ7V/wHOtHizICRyWGZh0HhS/VAlbmZfS2ovMNdR5AwDWMLDz/xnm2UMCCcQd6P69RjNGPTA9Ic4qQ/QnbCNz/Rbu0cQRmPBb6m2U+8aSKJy7qbFzyEGc9AFJkRRIERRqCjADh38ELy2sqKruAxYrmwFaJo5EP5cflwYRjylzGpseZ6iIHK1e8b+bxgEqkcqji1xbRs/wDdhgw6D/KnBrkpbj/ee6KH8ONYx7XOPy8leCGYoFZWXKU5P7VbCdR6UDZR9xwNX93Dgf4U4LD8slbnQzjlxkxt8xUsto51TJm961dRTwk5njYMvAjxubHF/bEfOFScSfGSDmkGkdI4Mv17Ai4lX0AfRH4qiI3MifiIf+pbuClAUAAAagBgAKXjD+Ko+Ib8XEJJuIhq2uKm4uZbaZjo2Rt8uAeO3FTpps7YPN8gaTCW5Jjg5o10npPAuI4YVHGeRgqj2kkVdPdONUCEj3nCty4oRy5mLn3DCt0bl8rRFbrkfpHVoYgdLztk6fbiavRLLFiVjjUhcSNp4YxVgQa0qTveg5Q+xqbjWd3JH/tcBx/K/wBS4efqxkj4q0zXDHoXN5EUKFQJIhGdXUMPcasjA59KFiv6ZxV006RYBmYaG2AjThRKm+ZZEiPoKuYHpoUKFKCrAhgdYIwINEqIpVntX2qc492g1omjBYclhmZeg77g38q5z9ip1/3Go2nj8arzIWIMi44sMra1BBZmICNFAHi8nMUIGzeFKfESHN+E7N4YgjPjSlYFlIXDRiNOTzY1L/6pBhBKx/iqPRJ5Q32+ptkPSRponLuZiXbkIM56AKjCRQxhEXYFGFChvEmWCQTiP7QLmK1ctBDI+SZAAcCduNWzXD7Zmx/QYCraKJdQRQv7eS9NcD0b2lMlh0Gj58McwH9hyT/KnNBYg9MjGhpiL9LsW3hwRvmjRpx9MnBEW1BoLUpNjbMGlJ0SvpCd6hgMKNGjRpMbmxxfnMR86n+ruOPBzSLpHSN4h7+ZT4mPk6stqlcxZeXdXBz6T8RqFIJrCElNQeNc7K1X/io5mDOjIrjEawGxwrdOHs8XdrdOHs8XdrdCF0bSDbx92p4uqWrmMB1K4iMA56RZLe1jGMbek0gIFM5s5GxglBzo3IJGsU4XdCJetUekOcUeO/FT2mmznB5vkDSYS3XEh5o119Jo0d470WFndOfGKNEcp+RqTG7t1GBOl4xro7xo0aO8aNGtKOD0HNvelEw6cKOa4tpovcMv5fypxEIhiHQgr+nBGvSq4UaNGjRo0aNChQpwsUSkt+wA5zqpcZrhwka6o0GfoApeJGudtbudLHhLirAgg6wRgQaJRUkE9q/4ScQOjQawee5jwjgBzmQZmHsBqQvLI2XNKfNjWo8EQZydLMdLGjg0sfiR/wC7xatY5obW1AwkUMA0p563GsuoT/FbjWXUJ/itxrLqE/xW5dojjjuViUZjVrErrFlgqgB4pxr+rbBulGqISQyrgwP7jYRqqRwofLtbgawPmKwjltYSZoxrPKXmNE5VzNjIeRGM5PQKQLFFGqIo1KowA4UYeGZSrD9iKbCWBw8b6pIz3qPFccZeSwzEHgjg64yekDHfzZN8Iz7Hxj/lc/jd1GToD5PlcVgtwGb8bsKKvc3q4qRnCRcn27fIJjc2OL+2M+cKgeaTJZgi6gucmmwtrwiNzyHHmneP8acyEc0a/wDNLnu7vJB2rCN/OqIW9wxo4u5JNf1IJE/MuFf1o5oz+Ut8t5gYrVsuZtsp1dAq3eHx0Syx5Q85GGIIpPrrviQ80S94+QKpdWaNLG5zYqM7IaxaC8cIy7G1N5PUxHu3swhv0l6A+V/K5/G7o+NPS+V5WPGeBfrQPTj/AOKl+pmYtbMfRk5HT5AAgg4g7DqNbnwQPKeOY1AxqPCzvGLjDQkullqTG8tcI5trbHps1tbqOl+NQwZrYTN7ZuPv+lGw94383it0TEelsimH0h+Jbrtc6+igXtoT465Y+nnxCe1qsIZ1iOKB182kVERQFVQAAAMAAB5CTjPg12w1DVHSZ2BW2B2aC/k/tCffn3tYU/pX9aCN/wAy4/yebxUEj/lXGvRLt7lPkhQoUMQQcRWUtvK3jLdx6BGcr0UR9LhwS4X8XL6aFChQoUKFCsxcYxvyHGdWFRsvi5DBdx7V10csXl9HDERrUsI1oYLGgRfYow3xQ81yPccN7MVuhMOnj0Ge3QiG1Qelj6WG00AbhuPcONch7ugUOAKG/gZfMgQ+nIdFOzh2M11KdhP7mkCoihVUagBgAPJ6wp/TDe9KFT+pFelYQfCB/J+jYT/Aa9C3kb9hvjgjfNGjRAfzon5LilYIGMN3FtX/ACKkDxyKGRhnBBGIIo0aNGjRo0aTOuEd2Bs1PQxWBmuD/wC0MRwftMehhjvf1YI36RxaT6mBiluDrk1t0Ud40aNHeNMAoBJJ2DWaLNaQExWyD0ycxbpoD6TLg87fi5PRR3jRo0d40aNGtcY/Q72uIj3HGtUBT8jlf5PXalPzsFr0bMgdLrRo0aNGjRo0aFChvx8ZAFuANa6A9SZwC1ox97R+RB8VPE0b4bGrNPZTA80sZ+TCnyobiMMvNqKnnHA9ONT0gYUwVEUszHUAMajLNNIIrdNiimyhBEELco6SfIyYXFyuM5HoRbPa1J9XEStuDrbl9G8OCOCK1hh7t7Y4/avQknT/APcn+T9MwL//AFWtUMY958uuUjqVZTrBGBBp2VVcTWko5u7RAmAyJ4+TIPIx43lkhJA0vFrHRUuFreuDAToSbZ7H4GtGHuz/ADp85Ae4I96pUf106lbYH0Yz6XtbyJzRrxE5bnMq05MlxIZZpNSL/wAaBSBIolCIvMB5XlH9d7VIw94r0L2Yfsf5P07mAfrRkBmEeRkJlZlxp7jq6a46umuOrprjq6a46qnuOrprjq6a46umuOrprjqqefqqa46umuOqp7jq6a46qnmFwmLQOYtDbPYaLGzmAS4RebQw5xT3XU0911NPddTT3XU0911NPddTT3XU0911NPc9TUkgtncSIGUo0bHOQKNyL9IwkxSLKDlfT6akvOoqS86ivHvJGjFFeIri5qd/o7zGS4ZRlM2sge2jcKiKFVRDmAFPddTT3XU0911NPddTT3XU0911NPddTT3XU0XWwgGMasMks7aWNNMbyY4ysI8cANCimuOrprjq6a46qmuOrprjqqa46umuOrprjqqa46umuOqprjq6a46umuOqqSbEPjnjqWT8hp2LrIDnUjNhhXo7pP8ArGn8nK8YdldJF0oy5wa8KH7KO/XhQ/ZR368KH7KO/XhQ/ZR368KH7KO/XhQ/ZR368KH7KO/XhQ/ZR368KH7KO/XhQ/ZR368KH7KO/XhQ/ZR368KH7KO/XhO/ZR368KH7KO/XhQ/ZR368KH7KO/XhQ/ZR368J37KO/XhQ/ZR368KH7KO/XhQ/ZR368KH7KO/XhO/ZR368KH7KO/XhQ/ZR368KH7KO/XhQ/ZR368J37KO/XhQ/ZR368KH7IO/XhQ/ZB368KH7IO/XhQ/ZB368KH7IO/XhQ/ZB368KH7IO/XhQ/ZB368KH7IO/XhQ/ZB368KH7IO/XhQ/ZB368KH7IO/XhQ/ZB368KH7IO/XhQ/ZB368KH7IO/XhQ/ZB368KH7IO/XhQ/ZB368KH7IO/XhQ/ZB368KH7IO/XhQ/ZB368KH7IO/XhQ/ZB368KH7IO/XhQ/ZB368KH7IO/XhQ/ZB36uHnaSUyyysApZiMNGr/AOcr/8QAOREAAQMBBwEFBgQFBQAAAAAAAQACAwQFEBESICExMgYwQEFyEyIzQlFSFDRxkRYjYaGxFVBTYoD/2gAIAQIBAT8A/wDeQB8llP0WSRYHz/2Wsq20NPnK/iIf8X91/rzP+NUlW2thzj/Ysd7+0f5SH1XDhdn/AIM/qF5Pjy3a/tH+Uh9Vw4XZ/wCDP6heB44bYuPSOVS2qypq305Ow4RGBwQ3K7SYCkgI+64Ls9l9lUY+WC4KGJIAVXaLKadkQPPK2cMRwePG2vWfhKRzR1ScKnmfBOyUHccqCYVMEcoWzRiSrfrop2tij8nXBWJWRQGRj/nwQwcMQU+QQRSPKqZ3TzvlJ54VkVX4mmDCfeZz4weePA5Vs1n4qqeB0s4usCvbGHwyHZWrbDpnOigODESTcLrNtMwO9nIfcVtVzSxkMJ2dzdZlUaapYcdncrZwaRwfF2vV/hKRwHVJwicSceTzcC5pxYcCsccxN4u9XCLnHzxJ4ux3Dh5qyKk1NK5h6meL7SROfDDKB08r6EahxpGB3K7PxuaJpD0+Xi6mBtVTyRkdXCnhME0jD5ahxpjiMskbAqaFtNTRxAb+fiwu0NHg5tSBsW76QfqEDtsNNhUud7p3DZowCO58Z2gkDaEMPJdtphpaibhhT6WohG7DpsF4dSyMHLTuuB4vzXaWfGaGL6Nx/e8KyLLNW/M8fy2qKmhhZlACfCyVu4CtWzDTkyMHunnRYE2Wokj+5v8AhHlY+KaN1bUvtLQmP2nL+19NCaieNg81S07aaBkbRhlWDccSueFPCJ4nsd0nhVMJp55GG+zJPZ1sDvudh+6csPEDkqS2qGN725vmX+u0X1VTJ7WeV/1djf2dps9S+bya3+502/BklZKOCMt8Tskkbvom2xTYJtsUznNaj9fD/K9TkumkOPzrH+q4B/S/s7Fkonv83O/sNNuRZ6LN5sdjfzsvewUZc17HIfDjPh/lf6VL8WT1abGbls+n9J02k3NRVHpOlnUxDoi9Ph/lf6VL8WT1abL/ACFP6RprvydR6TpZ1MQ6IvT4ccP9Km+LJ6tNkOzWdTH6t02icKOd31adLOpiHRF6fDjh/pU3xZPVp7PSZ6LL9jtNtyZKF4+rhpZ1MQ6I/T4dvKlsGF8sj/qv4ehw6lNH7OaSP7HYftf2bnDJZIievdY+RvJI2C7QTAuihB/qb4253sb9XJthMwUdhwtfGUfDjlZkHDHpVrx+zr6jAcux/e+jndT1EcgPBxUEonijeL5JRFHI8qsnNRPLIT54i+zY/aVkDf8AsiXAou28UF2jhLamKTDZzf8AGixbU9gRDKfcTZGPGYOCc9rRmc7BWvaXtP5MR20WDEX1Zk8mD/KPjO0MQfRiTzY7D99G4OJ4VPadTTjLG4qa0qicYPcQiSTj56LAjDaaWTzLv8eMwJOULtFVtDGU7TzudQ402BVAZ4HHncIg9J8XLKIIZJSqqd1RO+Rx5OI1DjTBM6GaOVp6DiVFKKiCOUeL7RTmGGKEcP1jjVYExkbLCeGjHxduUn4mjD/mj1jjVYtL7GmL/mfv4vqBYVatIaSqeMPdPH6LbkqwrN/FF8jx7o4Vp2U+lkcWDGNbHfzuHC32x5Ks+zpKiRpcPcVs0DKdjJWDnlYDAEKgpjV1UUfyjn9F0tDB4vg4q26MVNL7UD3mc/ooojNLGwKlpxTU0cTdiOU9jJWFkgxCtyzoqXJJHwbhwFY9EKtz5JOGJjY4WhjBspoo6iF8bt8eFNEYZZGFWLSCGAyke8/j9Ed9/GdQLCqKx2wVskx4HSjubu0f5WH1XDhdn/g1HqFw2KrbLE9VFKOD1roaGN8bwcUXF2A+i4F3aP8AKw+q4cLs/wDBqPULuQg8jEfVf1PjfJBvulcG7tGcKSIeWdb/AGoY/auz3wZz/wBruSi33fHN5UbMY3J7cDdaFH+OphH5hfw9N96Fgy/erPpPwULm/V1zG7pzfdTurxsTcxTdgp2LgoHBYrFOK5KhZdI3DxtO3dclSDM1PbgdLG4lMZlbdK3xRuY3dRjI1t87Fwb+SoGb43EbojEJ7d7h4cBBqbygcWtvkGZqe3A3sbiVGMrLnDZY4BOO6LUR4Q3BuKG18JxZhonYjsVyVAzF2iT3QjdyEW+DCDUNkb6c4HDQ8ZmqRuBTG4lRjK3RO7dG4bG4tTvABuKa283wnB+mdm+ZQx76DwpTi68c3ndFvfNahtoN7TgU3duic7YKndtoecAnG8c3hcot7xvVpOmE4sw0VD8SoH4HRMdtIR0P57uPSdNOcDhfK7K1POJTDgVC7M2+Y76Qhcbn8d2zjuoTg+4Kd+6KCp5MDdjgpDi68ain9Pds47oHApvSpHZWqQ43FRbFRuzNTzg1E43jUU7p7tvDdZ0RHFjVIc78FNGAVlCLQoWAuCZ/LfgpjtoGpy8nd23hus6In4ByiGJzKpJxWYokqmJLmqYfMnvxGganLyd3beNR0hQHFuCqD7xuKpupSnBmkIaXId23jUdIUBwdgpz7xucoOpTnHbSENLkO6HKGs6WHByl5ucoupPOJ0jUeEEee5HKGs6n83OUfVqGo8II89y3lqHcHS+48KIYnHSNZ4QR57lvLUO4Ok9Nx4TBg3SNZ4QR57lvPckLBYLBYLBZFkWCwWCwWCA7jBP2d3Pms6zLOsyzrMsyzIPWdZ1nWdZ1mWZZ1nWdZ1nWdZlmWZZlmWZZlnR3/APcv/8QANREAAQIEBQIEBAUFAQEAAAAAAQACAxESMQQFECAhMkATIjBBM1FSchVCYXGBFFBigLEjNP/aAAgBAwEBPwD/AHymJTKqaqm/SgQf7LChGPEpC/DivwV31rFYV2Fi0/2XL/jP+3RtmrPPiwP5/sU9cv8AjP8At0bZqzz4sD+dZ98T7KJhSyE16EpTGmXy8Z4H06Ns1Z5PxIEv1U5hGXusLl7sRBe8i1kAW8G7e9wkLxYvNlEYHscxFhhPe0qc1gIJhlzj8tG2as3wsWNQ+H7TXLTIhMhmNFhsCgQmwYLGAWus0w/gYisDyu7wn2WDheHC0x0AuLXNWFwghis31bZumPy0RW1wx51lGDcHmM8c6ZhhhHgOHuuQZG7e7wkLxYvKHHGhAkAdjbN0vZBrROkSGkuCFmmHGHxNY6Xd3l7wHPaibDc2zdvI4Czx7S6DDH892x5hRAVDeHsDtws3bEiCFDiPKjxXYiM9xP7d25ZfGmHQj7W2hNkRztznE0sbAF3XQ7zAtqj1D2vtdFa33ULEwYg4I250wtxEN5s63eeyy5vkc/W8wsXiRCFITojnOuUyM6E7glZbmIxAEN58wsr31zuHVAY6Vnf9Xt3U0TwsG2mAzWI6hjnKK8xHzKBQUGKYURjm9QusPGEeDDeNcwbXhIo+Tf8AiFlPuDZNwkUtHC/ooqhtpYxuuYRKYbW/VtySMXQ3wvkatYja4cRnzTsqxFSdlkcNLpIWcO3PUEzhrVzszB1UZrfp25NEoxcvrEtbcryp/LHCSPU/tzdqb0t24szxETbl7qcXB+4bXdLkep/3dubtTelu3E/HibcH/wDTA+4bXdLkep/3dufypnS3bi+MQ/8AU7cAKsXAb8nA7XdLkep/3dufypnS3bmDZRgfm3blDKsWx3yBO13S5Hqd93blMxzmtX4g6aY6pjDrmLCWNePZSB1kCJlZHBIbFjEfoNXuoY93yajnLpp+bucySHb+yk1ECSwjqoDDrFYIkNzE9hY6WrGGK+GwLCwhAgQoY+Ujrj3+HhIx/wAUA0hSb3eXOnDLfp2YzC1+cXThQbJrXP4DVleX+H/7RBzszmIG4YM+sod5gXUxnN+rZ+9k/DQXmygYDDwuSEAAJG2zOohdHhM+TZod2ZANKwEIzdEO4WbtzrDHyRm+3BXBHHdtaXva1QmCExrdws3bGhiNBfDcOsSCiQzAjRGHu8AyqI8/LeLN3Z3BDHQowu7u8HF8OLT9W8WbuzbE+LHDPZvdlYaN4sKfuEFjcR4VLQsNiWxBSSvaejbNXEi4LHZgzDsk0+dZVjfHc9jzay5nIrHRxh8O9/ubfuuXGo95g43hxZGzk91LZqJE8WI4lCphmCsHiDFNJ0FmrNMWcMGNZ7pznxX1uPKgxTBjMe3iV1CiiNChvCzbFeLGoB8rL/uhwJd4VFxfiQWsQ0y74r/t0FmrPPiwP50KwmYmDh4rD7dK6nF3fBsiNcu+K/7dBZqzz4sD+dS0Ej9EOOO+J8yB0wHxnj/FSKFUupZ38WANJoO74oNm1NOkCN4EWo/SvxAIZy2XSsbi/wCrisd8tCqZNQ713JkmpwpdoRNUqlAS0YKiihwZd6zk1aOFQmhtKYKWzQUTgz7so2TBIavEjsKYKjq4TagZd0XKfKBmNXCYQ1cmCls9bKaa7ty5HWH07HiRQ0aKnT2PPlQGg4QPaFyLtkI7HCYTUUwUtnsilA6HlAoO7EuRcnFC6OsMydteJGpMEzVsKiGZ2G+gcgfWLkXai6Oo4TdkUzElCMuNhRMzqEb6gprvUdZE7G32sPl2dT0RS9TmNXny7XX2BMt6cSylsbfbC1e6TUzgTThMKGZjWKdrr7CmW9OJtBkgZoiesM+ZHR3mcgih5X6BRDN2slOSPOwWTL+nE3AyTTqOEE40tTdAnphm1FHnUlTntFky/pxL7xbZD5anGp0k/hwUyg8q7gmml0k/hux3LdwTb+nEvv8AbZDdJMH5k/lwXCAClKIE8fmT3TbsG4Jt/Tffc0TKO2EfLJHrOjUesKIfJLaE6+1qbf0333NMijthHzL850an3aop2hOvtam39N994ttbdNvo1RE6+323C6F0Lek++9ttoumW0aolkb7TbcLoXQt6JsnX3t6l77WnjQXUS2433C6F0LeibJ1/Q9trb6C6iX3e+4XQuhb0XWRCkqVJSVKpVKbbdUq0dp6VSqVSqVSqUG6Nt6PsqFSpKlUqlUqlU8qlUqlUqlUqlBqpVKpVKpVKpVKpVKpQaqV7f7y//9k=";
        private readonly MovieHandler _movieHandler = new(new FakeMovieRepository(), new FakePersonalListRepository(), new FakeStorage());
        private GenericCommandResult _result = new();
        private readonly CreateMovieCommand _validCreateMovieCommand;
        private readonly CreateMovieCommand _notExistentPersonalListIdCreateMovieCommand;
        private readonly CreateMovieCommand _aPersonalListNotAssociateWithUserIdCreateCommand;
        private readonly CreateMovieCommand _differentCategoryTypeCreateCommand;
        private readonly CreateMovieCommand _invalidCreateMovieCommand;
        private readonly UpdateMovieCommand _validUpdateMovieCommand = new (0, "Disney", "Russo Brothers", 149, "Avengers: Civil War", "Action", 2018);
        private readonly UpdateMovieCommand _notExistentIdUpdateMovieCommand = new(-1, "Disney", "Russo Brothers", 149, "Avengers: Civil War", "Action", 2018);
        private readonly UpdateMovieCommand _invalidUpdateMovieCommand = new(0, "", "", 149, "", "", 2018);
        private readonly UpdateMovieCommand _aMovieNotAssociateWithUserIdUpdateCommand = new(0, "Disney", "Russo Brothers", 149, "Avengers: Civil War", "Action", 2018) { UserId = -1};
        private readonly GetMovieByIdCommand _getMovieByIdCommand = new();
        private readonly GetMovieByIdCommand _getANotExistentMovieByIdCommand = new() {Id = -1};
        private readonly GetMovieByIdCommand _getANotAssociateUserIdCommand = new() {UserId = -1};
        private readonly DeleteMovieCommand _deleteMovieCommand = new();
        private readonly DeleteMovieCommand _deleteANotExistentMovieCommand = new() { Id = -1 };
        private readonly DeleteMovieCommand _deleteANotAssociateUserIdDeleteCommand = new() { UserId = -1 };
        private readonly GetAllByPersonalListIdCommand _getAllByPersonalListId = new();
        private readonly GetAllByPersonalListIdCommand _getAllByPersonalListIdNotAssociateUserIdCommand = new() { UserId = -1 };
        private readonly SwitchPersonalListFromItemCommand _switchPersonalListFromItemCommand = new();
        private readonly SwitchPersonalListFromItemCommand _switchItemIdNotAssociateUserIdCommand = new() { UserId = -1 };
        private readonly SwitchPersonalListFromItemCommand _switchPersonalListNotAssociateUserIdCommand = new() { ItemId = -1, UserId = -1 };
        private readonly SwitchPersonalListFromItemCommand _switchANotExistentNewPersonalListCommand = new() { NewPersonalListId = -1 };
        private readonly SwitchPersonalListFromItemCommand _switchANotExistentNewMovieIdCommand = new() { ItemId = -1 };
        private readonly SwitchPersonalListFromItemCommand _switchADifferentCategoryTypeCommand = new() { NewPersonalListId = 1 };
        private readonly int _existentPersonalListId = 0;
        private readonly int _notExistentPersonalListId = -1;
        private readonly int _personalListIdWithDifferentCategoryType = 1;

        public MovieHandlerTests()
        {
            _validCreateMovieCommand = new CreateMovieCommand("Disney", "Russo Brothers", 149, "Avengers: Civil War", "Action", _base64Image, 2018, _existentPersonalListId);
            _notExistentPersonalListIdCreateMovieCommand = new CreateMovieCommand("Disney", "Russo Brothers", 149, "Avengers: Civil War", "Action", _base64Image, 2018, _notExistentPersonalListId);
            _aPersonalListNotAssociateWithUserIdCreateCommand = new CreateMovieCommand("Disney", "Russo Brothers", 149, "Avengers: Civil War", "Action", _base64Image, 2018, _existentPersonalListId) { UserId = -1};
            _invalidCreateMovieCommand = new CreateMovieCommand("", "", 149, "", "", _base64Image, 2018, _existentPersonalListId);
            _differentCategoryTypeCreateCommand = new CreateMovieCommand("Disney", "Russo Brothers", 149, "Avengers: Civil War", "Action", _base64Image, 2018, _personalListIdWithDifferentCategoryType);
        }

        #region CreateMovieCommand
        [TestMethod]
        public async Task ShouldReturnSuccessWhenCreateCommandIsValid()
        {
            var res = await _movieHandler.Handle(_validCreateMovieCommand);
            _result = (GenericCommandResult)res;
            Assert.IsTrue(_result.Success);
        }

        [TestMethod]
        public async Task ShouldReturnFailWhenCreateCommandIsInvalid()
        {
            var res = await _movieHandler.Handle(_invalidCreateMovieCommand);
            _result = (GenericCommandResult)res;
            Assert.IsFalse(_result.Success);
        }        

        [TestMethod]
        public async Task ShouldReturnFailWhenCreateCommandHasANotExistentPersonalListId()
        {
            var res = await _movieHandler.Handle(_notExistentPersonalListIdCreateMovieCommand);
            _result = (GenericCommandResult)res;
            Assert.AreEqual(_result.Message,"Não foi possível adicionar o filme à lista");
        }

        [TestMethod]
        public async Task ShouldReturnFailWhensPersonalListFromCreateIsNotAssociatedWithUserIdCommand()
        {
            var res = await _movieHandler.Handle(_aPersonalListNotAssociateWithUserIdCreateCommand);
            _result = (GenericCommandResult)res;
            Assert.AreEqual(_result.Message, "Não é possível criar o filme na lista informada");
        }

        [TestMethod]
        public async Task ShouldReturnFailWhensPersonalListCategoryTypeItsNotEnumMovieCommand()
        {
            var res = await _movieHandler.Handle(_differentCategoryTypeCreateCommand);
            _result = (GenericCommandResult)res;
            Assert.AreEqual(_result.Message, "Lista informada não é da categoria de filmes");
        }
        #endregion

        #region UpdateMovieCommand
        [TestMethod]
        public async Task ShouldReturnSuccessWhenUpdateCommandIsValid()
        {
            var res = await _movieHandler.Handle(_validUpdateMovieCommand);
            _result = (GenericCommandResult)res;
            Assert.IsTrue(_result.Success);
        }

        [TestMethod]
        public async Task ShouldReturnFailWhenUpdateCommandIsInvalid()
        {
            var res = await _movieHandler.Handle(_invalidUpdateMovieCommand);
            _result = (GenericCommandResult)res;
            Assert.IsFalse(_result.Success);
        }

        [TestMethod]
        public async Task ShouldReturnFailWhenUpdateCommandHasANotExistentId()
        {
            var res = await _movieHandler.Handle(_notExistentIdUpdateMovieCommand);
            _result = (GenericCommandResult)res;
            Assert.AreEqual(_result.Message, "Não foi possível atualizar as informações do filme");
        }

        [TestMethod]
        public async Task ShouldReturnFailWhensMovieFromUpdateIsNotAssociatedWithUserIdCommand()
        {
            var res = await _movieHandler.Handle(_aMovieNotAssociateWithUserIdUpdateCommand);
            _result = (GenericCommandResult)res;
            Assert.AreEqual(_result.Message, "Filme indisponível");
        }
        #endregion

        #region GetMovieByIdCommand
        [TestMethod]
        public async Task ShouldReturnFailWhenGetANotExistentMovieByIdCommand()
        {
            var res = await _movieHandler.Handle(_getANotExistentMovieByIdCommand);
            _result = (GenericCommandResult)res;
            Assert.AreEqual(_result.Message, "Não foi possível obter o filme");
        }

        [TestMethod]
        public async Task ShouldReturnSuccessWhenGetAExistentMovieByIdCommand()
        {
            var res = await _movieHandler.Handle(_getMovieByIdCommand);
            _result = (GenericCommandResult)res;
            Assert.IsTrue(_result.Success);
        }

        [TestMethod]
        public async Task ShouldReturnFailWhensMovieIsNotAssociatedWithUserIdCommand()
        {
            var res = await _movieHandler.Handle(_getANotAssociateUserIdCommand);
            _result = (GenericCommandResult)res;
            Assert.AreEqual(_result.Message, "Filme indisponível");
        }
        #endregion

        #region DeleteMovieCommand
        [TestMethod]
        public async Task ShouldReturnFailWhenGetANotExistentMovieDeleteCommand()
        {
            var res = await _movieHandler.Handle(_deleteANotExistentMovieCommand);
            _result = (GenericCommandResult)res;
            Assert.AreEqual(_result.Message, "Não foi realizar a exclusão do filme");
        }

        [TestMethod]
        public async Task ShouldReturnSuccessWhenGetAExistentMovieDeleteCommand()
        {
            var res = await _movieHandler.Handle(_deleteMovieCommand);
            _result = (GenericCommandResult)res;
            Assert.IsTrue(_result.Success);
        }

        [TestMethod]
        public async Task ShouldReturnFailWhensMovieIsNotAssociatedWithUserIdDeleteCommand()
        {
            var res = await _movieHandler.Handle(_deleteANotAssociateUserIdDeleteCommand);
            _result = (GenericCommandResult)res;
            Assert.AreEqual(_result.Message, "Filme indisponível");
        }
        #endregion

        #region GetAllByPersonalListIdCommand
        [TestMethod]
        public async Task ShouldReturnSuccessWhenGetAllByPersonalListIdCommand()
        {
            var res = await _movieHandler.Handle(_getAllByPersonalListId);
            _result = (GenericCommandResult)res;
            Assert.IsTrue(_result.Success);
        }

        [TestMethod]
        public async Task ShouldReturnFailWhensPersonalListIdIsNotAssociatedWithUserIdCommand()
        {
            var res = await _movieHandler.Handle(_getAllByPersonalListIdNotAssociateUserIdCommand);
            _result = (GenericCommandResult)res;
            Assert.AreEqual(_result.Message, "Lista indisponível");
        }
        #endregion

        #region SwitchPersonalListFromItemCommand
        [TestMethod]
        public async Task ShouldReturnSuccessWhensSwitchPersonalListFromItemCommand()
        {
            var res = await _movieHandler.Handle(_switchPersonalListFromItemCommand);
            _result = (GenericCommandResult)res;
            Assert.IsTrue(_result.Success);
        }

        [TestMethod]
        public async Task ShouldReturnFailWhensItemIdFromSwitchIsNotAssociatedWithUserIdCommand()
        {
            var res = await _movieHandler.Handle(_switchItemIdNotAssociateUserIdCommand);
            _result = (GenericCommandResult)res;
            Assert.AreEqual(_result.Message, "Filme indisponível para troca");
        }

        [TestMethod]
        public async Task ShouldReturnFailWhensPersonalListIdFromSwitchIsNotAssociatedWithUserIdCommand()
        {
            var res = await _movieHandler.Handle(_switchPersonalListNotAssociateUserIdCommand);
            _result = (GenericCommandResult)res;
            Assert.AreEqual(_result.Message, "Lista indisponível para troca");
        }

        [TestMethod]
        public async Task ShouldReturnFailWhensANotExistentNewPersonalListItsInformedCommand()
        {
            var res = await _movieHandler.Handle(_switchANotExistentNewPersonalListCommand);
            _result = (GenericCommandResult)res;
            Assert.AreEqual(_result.Message, "Lista não encontrada");
        }

        [TestMethod]
        public async Task ShouldReturnFailWhenSwitchToADifferentCategoryTypeCommand()
        {
            var res = await _movieHandler.Handle(_switchADifferentCategoryTypeCommand);
            _result = (GenericCommandResult)res;
            Assert.AreEqual(_result.Message, "Não é possível realizar a troca para a lista informada");
        }

        [TestMethod]
        public async Task ShouldReturnFailWhensANotExistentMovieItsInformedCommand()
        {
            var res = await _movieHandler.Handle(_switchANotExistentNewMovieIdCommand);
            _result = (GenericCommandResult)res;
            Assert.AreEqual(_result.Message, "Filme não encontrado");
        }
        #endregion
    }
}
