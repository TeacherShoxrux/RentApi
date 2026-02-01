namespace RentApi.Data;
public class ShiftCalculator
{
  public double CalculateShifts(DateTime start, DateTime end)
  {
    double shifts = 0;

    // 1. Agar start 20:00 dan keyin bo'lsa -> Tungi smena boshlandi
    if (start.Hour >= 20)
    {
      shifts += 1;
      // Tungi smena tugash vaqti: Ertasi kuni 10:00
      var nextMorning = start.Date.AddDays(1).AddHours(10);

      // Agar mijoz 10:00 dan o'tib ketsa
      if (end > nextMorning)
      {
        // Qancha o'tib ketdi?
        // Agar yana kechgacha qolsa +1 smena
        shifts += 1;
      }
    }
    else
    {
      // Kunduzgi smena logikasi...
      shifts += 1;
    }

    return shifts;
  }
}