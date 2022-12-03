import clr

from datetime import datetime
from params import get_length

def created_on():
    """
    Gets the current date and time with COBie format

    :returns: string formatted date and time in the form of 2000-01-01T08:10:10
    """
    dt_now = datetime.now()
    year = dt_now.year
    month = dt_now.month
    day = dt_now.day

    yy_mm_dd = str.format('{0}-{1}-{2}', year, month, day)

    hour = dt_now.hour
    minute = dt_now.minute
    second = dt_now.second

    hh_mm_ss = str.format('T{0}:{1}:{2}', hour, minute, second)

    date = str.format('{0}{1}', yy_mm_dd, hh_mm_ss)

    return date

def get_size(et, *args):
    """
    Retrieves the values from the parameter names to get the size as description

    :param et: the element type
    :param args: parameter names
    :return: size in string
    """
    res = ''

    if len(args) == 3:
        res = str.format('{} x {} x {}', get_length(et, args[0]), get_length(et, args[1]), get_length(et, args[2]))

    else:
        res = str.format('{} x {}', get_length(et, args[0]), get_length(et, args[1]))

    return res
