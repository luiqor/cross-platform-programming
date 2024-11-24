import { Container } from "react-bootstrap";
import { Lab } from "../../components/components";

const Lab3 = () => {
  return (
    <Container className="mt-5">
      <Lab
        index={2}
        endpoint={"/api/lab/lab3"}
        defaultValue={"3 3 3\n1..\noo.\n...\n\nooo\n..o\n.oo\n\nooo\no..\no.2"}
      >
        Розплющивши очі, Принц Персії виявив, що знаходиться на верхньому рівні
        підземного лабіринту Джаффара. Лабіринт складається з рівнів h,
        розташованих строго один під одним. Кожен рівень є прямокутним
        майданчиком, розбитим на m х n ділянок. На деяких ділянках стоять
        колони, які підтримують стелю, на такі ділянки Принц заходити не може.
        Принц може переміщатися з однієї ділянки на інший сусідній вільний
        ділянку того ж рівня, так само він може проломити підлогу під собою і
        виявитися рівнем нижче (на самому нижньому рівні підлогу проломити не
        можна). Будь-яке переміщення займає у Принца 5 секунд. На одній із
        ділянок нижнього рівня на Принца чекає Принцеса. Допоможіть Принцу
        знайти Принцесу, витративши на це якнайменше часу. Вхідні дані У першому
        рядку вхідного файлу INPUT.TXT містяться натуральні числа h, m і n —
        висота та горизонтальні розміри лабіринту (2 ≤ h, m, n ≤ 50). Далі у
        вхідному файлі наведені блоки h, що описують рівні лабіринту в порядку
        від верхнього до нижнього. Кожен блок містить m рядків, по n символів у
        кожному: «.» позначає вільну ділянку, «о» позначає ділянку з колоною,
        «1» позначає вільну ділянку, в якій опинився Принц на початку своєї
        подорожі, «2» позначає вільну ділянку, на якій нудиться Принцеса.
        Символи "1" і "2" зустрічаються у вхідному файлі рівно по одному разу:
        символ "1" - в описі найвищого рівня, а символ "2" - в описі самого
        нижнього. Сусідні блоки розділені одним порожнім рядком. Вихідні дані У
        вихідний файл OUTPUT.TXT виведіть мінімальний час у секундах, необхідний
        Принцу, щоб знайти Принцесу. Оскільки добро завжди перемагає Зло, то
        гарантується, що Принц може це зробити.
      </Lab>
    </Container>
  );
};

export { Lab3 };