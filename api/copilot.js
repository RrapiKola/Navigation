function calculateDaysBetweenDates(begin, end) {
  const date1 = new Date(begin);
  const date2 = new Date(end);
  const timeDiff = Math.abs(date2.getTime() - date1.getTime());
  return Math.ceil(timeDiff / (1000 * 3600 * 24));
}